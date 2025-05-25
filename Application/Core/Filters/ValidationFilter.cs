using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Application.Core.Validators.OwnershipValidator;
using Application.Core.ApiResponse;
using Microsoft.AspNetCore.Http;
using Application.DTO.General;
using System.Threading.Tasks;
using Application.DTO.Researches;
using Application.DTO.LabTesting;
using System.Linq.Expressions;
namespace Application.Core.Filters
{
    public class ValidationFilter : IActionFilter
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IOwnershipValidator _ownershipValidator;

        public ValidationFilter(IServiceProvider serviceProvider, IOwnershipValidator ownershipValidator)
        {
            _serviceProvider = serviceProvider;
            _ownershipValidator = ownershipValidator;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var endpoint = context.HttpContext.GetEndpoint();
            var hasAuthorize = endpoint?.Metadata?.GetMetadata<AuthorizeAttribute>() != null;

            Guid userId = Guid.Empty;
            if (!hasAuthorize)
            {
                return;
            }
            var userIdStr = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdStr, out userId))
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            var userRole = context.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;
            if (userRole == "Admin") return;
            foreach (var argument in context.ActionArguments)
            {
                var value = argument.Value;
                Console.WriteLine(argument);
                Console.WriteLine(value);
                if (value == null) continue;

                if(argument.Key.Equals("emailDto",StringComparison.OrdinalIgnoreCase) && value is EmailDto emailDto)
                {
                    var email = emailDto.Email;
                    try
                    {
                        _ownershipValidator.ValidateUserEmailOwnership(userId, email).Wait();
                    }
                    catch (Exception ex)
                    {
                        context.Result = Forbidden(ex);
                    }
                }
                if (argument.Key.Equals("createResearchDto", StringComparison.OrdinalIgnoreCase) && value is CreateResearchDto createResearchDto)
                {
                    var researchOwnerId = createResearchDto.OwnerId;
                    try
                    {
                        _ownershipValidator.ValidateAccountOwnership(userId, Guid.Parse(researchOwnerId)).Wait();
                    }
                    catch (Exception ex)
                    {
                        context.Result = Forbidden(ex);
                    }
                }
                if (argument.Key.Equals("createLabTestDto", StringComparison.OrdinalIgnoreCase) && value is CreateLabTestDto createLabTestDto)
                {
                    try
                    {
                        _ownershipValidator.ValidateAccountOwnership(userId, Guid.Parse(createLabTestDto.CreatorId)).Wait();
                        _ownershipValidator.ValidateResearchOwnership(userId, Guid.Parse(createLabTestDto.ResearchId)).Wait();
                    }
                    catch (Exception ex)
                    {
                        context.Result = Forbidden(ex);
                    }
                }
                if (argument.Key.Equals("userResearchDto", StringComparison.OrdinalIgnoreCase) && value is CreateUserResearchDto userResearchDto) 
                {
                    try
                    {
                        _ownershipValidator.ValidateResearchOwnership(userId, Guid.Parse(userResearchDto.ResearchId)).Wait();
                    }
                    catch(Exception ex)
                    {
                       context.Result = Forbidden(ex);
                    }
                }
                if (argument.Key.Equals("researchId", StringComparison.OrdinalIgnoreCase) && value is string researchId)
                {
                    try
                    {
                        _ownershipValidator.ValidateResearchOwnership(userId, Guid.Parse(researchId)).Wait();
                    }
                    catch (Exception ex)
                    {
                        context.Result = Forbidden(ex);
                    }
                }

            }
        }

        private ObjectResult Forbidden(Exception ex)
        {
            var response = ApiResponse<string>.Failure(ex.Message, 403);
            return new ObjectResult(response) { StatusCode = 403 };
        }
        public void OnActionExecuted(ActionExecutedContext context) { }

    }
}
