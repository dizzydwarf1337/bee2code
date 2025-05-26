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
using Application.Features.Users.Commands.EditUser;
using Application.DTO.Users;
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
                if (argument.Key.Equals("createLabTestResultDto", StringComparison.OrdinalIgnoreCase) && value is CreateLabTestResultDto createLabTestResultDto)
                {
                    try
                    {
                        _ownershipValidator.ValidateLabTestOwnership(userId, Guid.Parse(createLabTestResultDto.LabTestId)).Wait();
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
                if (argument.Key.Equals("deleteResearchId", StringComparison.OrdinalIgnoreCase) && value is string researchId)
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
                if (argument.Key.Equals("deleteLabTestId", StringComparison.OrdinalIgnoreCase) && value is string deleteLabTestId)
                {
                    try
                    {
                        _ownershipValidator.ValidateLabTestOwnership(userId,Guid.Parse(deleteLabTestId)).Wait();
                    }
                    catch (Exception ex)
                    {
                        context.Result = Forbidden(ex);
                    }
                }
                if (argument.Key.Equals("deleteLabTestResultId", StringComparison.OrdinalIgnoreCase) && value is string deleteLabTestResultId)
                {
                    try
                    {
                        _ownershipValidator.ValidateLabTestResultOwnership(userId, Guid.Parse(deleteLabTestResultId)).Wait();
                    }
                    catch (Exception ex)
                    {
                        context.Result = Forbidden(ex);
                    }
                }
                if (argument.Key.Equals("editLabTestDto", StringComparison.OrdinalIgnoreCase) && value is EditLabTestDto editLabTestDto)
                {
                    try
                    {
                        _ownershipValidator.ValidateLabTestOwnership(userId, Guid.Parse(editLabTestDto.Id)).Wait();
                    }
                    catch (Exception ex)
                    {
                        context.Result = Forbidden(ex);
                    }
                }
                if (argument.Key.Equals("editLabTestResultDto", StringComparison.OrdinalIgnoreCase) && value is EditLabTestResultDto editLabTestResultDto)
                {
                    try
                    {
                        _ownershipValidator.ValidateLabTestResultOwnership(userId, Guid.Parse(editLabTestResultDto.Id)).Wait();
                    }
                    catch (Exception ex)
                    {
                        context.Result = Forbidden(ex);
                    }
                }
                if (argument.Key.Equals("editResearchDto", StringComparison.OrdinalIgnoreCase) && value is EditResearchDto editResearchDto)
                {
                    try
                    {
                        _ownershipValidator.ValidateResearchOwnership(userId, Guid.Parse(editResearchDto.Id)).Wait();
                    }
                    catch (Exception ex)
                    {
                        context.Result = Forbidden(ex);
                    }
                }
                if (argument.Key.Equals("deleteUserId", StringComparison.OrdinalIgnoreCase) && value is string deleteUserId)
                {
                    try
                    {
                        _ownershipValidator.ValidateAccountOwnership(userId, Guid.Parse(deleteUserId)).Wait();
                    }
                    catch (Exception ex)
                    {
                        context.Result = Forbidden(ex);
                    }
                }
                if (argument.Key.Equals("editUserDto", StringComparison.OrdinalIgnoreCase) && value is EditUserDto editUserDto)
                {
                    try
                    {
                        _ownershipValidator.ValidateAccountOwnership(userId, Guid.Parse(editUserDto.Id)).Wait();
                    }
                    catch (Exception ex)
                    {
                        context.Result = Forbidden(ex);
                    }
                }
                if (argument.Key.Equals("getResearchesByPatientId", StringComparison.OrdinalIgnoreCase) && value is string patientId && userRole == "Patient")
                {
                    try
                    {
                        _ownershipValidator.ValidateAccountOwnership(userId, Guid.Parse(patientId)).Wait();
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
            return new ObjectResult(response){ StatusCode = 403, Value = response};
        }
        public void OnActionExecuted(ActionExecutedContext context) { }

    }
}
