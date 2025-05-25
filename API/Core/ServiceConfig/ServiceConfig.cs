using Application.Core.Factories.Implementations;
using Application.Core.Factories.Interfaces;
using Application.Core.Mapper;
using Application.Core.Validators.OwnershipValidator;
using Application.Features.Auth.Commands.Login;
using Application.Services.Implementations.General;
using Application.Services.Implementations.LabTesting;
using Application.Services.Implementations.Researches;
using Application.Services.Implementations.Users;
using Application.Services.Interfaces.General;
using Application.Services.Interfaces.LabTesting;
using Application.Services.Interfaces.Researches;
using Application.Services.Interfaces.Users;
using Domain.Interfaces.Commands.LabTestingCommands;
using Domain.Interfaces.Commands.LinksCommands;
using Domain.Interfaces.Commands.ResearchesCommands;
using Domain.Interfaces.Commands.UsersCommands;
using Domain.Interfaces.Queries.LabTestingQueries;
using Domain.Interfaces.Queries.LinksQueries;
using Domain.Interfaces.Queries.ResearchesQueries;
using Domain.Interfaces.Queries.UserQueries;
using Persistence.Repositories.Commands.LabTesting;
using Persistence.Repositories.Commands.Links;
using Persistence.Repositories.Commands.Researches;
using Persistence.Repositories.Commands.Users;
using Persistence.Repositories.Queries.LabTesting;
using Persistence.Repositories.Queries.Links;
using Persistence.Repositories.Queries.Researches;
using Persistence.Repositories.Queries.Users;

namespace API.Core.ServiceConfig
{
    public class ServiceConfig
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            // Commands Repositories
            
                // LabTesting
                    services.AddScoped<ILabTestCommandRepository, LabTestCommandRepository>();
                    services.AddScoped<ILabTestResultCommandRepository, LabTestResultCommandRepository>();
            
                // Links
                    services.AddScoped<IUserResearchCommandRepository, UserResearchCommandRepository>();
            
                // Researches
                    services.AddScoped<IResearchCommandRepository, ResearchCommandRepository>();
            
                // Users
                    services.AddScoped<IUserCommandRepository, UserCommandRepository>();
                    services.AddScoped<IUserNotificationCommandRepository, UserNotificationCommandRepository>();


            //Query Repositories

                // LabTesting
                    services.AddScoped<ILabTestQueryRepository, LabTestQueryRepository>();
                    services.AddScoped<ILabTestResultQueryRepository, LabTestResultQueryRepository>();

                // Links
                    services.AddScoped<IUserResearchQueryRepository, UserResearchQueryRepository>();

                // Researches
                    services.AddScoped<IResearchQueryRepository, ResearchQueryRepository>();

                // Users
                    services.AddScoped<IUserQueryRepository, UserQueryRepository>();
                    services.AddScoped<IUserNotificationQueryRepository, UserNotificationQueryRepository>();


            // Services 
                // General
                    services.AddScoped<IAuthService, AuthService>();
                    services.AddScoped<ITokenService, TokenService>();
                    services.AddScoped<IFileService, FileService>();

                // LabTesting
                    services.AddScoped<ILabTestService, LabTestService>();
                    services.AddScoped<ILabTestResultService, LabTestResultService>();
                //
                // Research
                    services.AddScoped<IResearchService, ResearchService>();
                // Users
                    services.AddScoped<IUserService, UserService>();


            services.AddScoped<IUserFactory, UserFactory>();

            services.AddAutoMapper(typeof(AutoMapperProfiler).Assembly);

            services.AddScoped<IOwnershipValidator, OwnershipValidator>();

            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(typeof(LoginCommand).Assembly);
            });

        }
    }
}
