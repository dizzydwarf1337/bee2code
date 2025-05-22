using Domain.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Persistence.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Seed
{
    public class DbSeed
    {
        public static async Task SeedDb(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<BeeCodeDbContext>();
                var userManager = services.GetRequiredService<UserManager<User>>();
                var logger = services.GetRequiredService<ILogger<DbSeed>>();
                logger.LogInformation("Starting database migration...");
                await context.Database.MigrateAsync();
                var adminRoleGuid = await context.Roles.FirstOrDefaultAsync(x => x.Name!.ToLower() == "admin");
                if ((await context.UserRoles.FirstOrDefaultAsync(x => Guid.Equals(adminRoleGuid, x.RoleId))) != null)
                {
                    logger.LogInformation("Admin already exist. Skipping seeding.");
                    return;
                }


                logger.LogInformation("Creating admin user...");
                var admin = new User
                {
                    Email = "admin@gmail.com",
                    LastName = "Admin",
                    FirstName = "Admin",
                    EmailConfirmed = true,
                    City = "",
                    Country = "",
                    Street = "",
                    HouseNumber = "",
                    PostalCode = "",
                };
                admin.UserName = admin.Email;
                var result = await userManager.CreateAsync(admin, "SuperSecretAdminPassword1!");

                if (!result.Succeeded)
                {
                    logger.LogError("Failed to create admin user: {Errors}", string.Join(", ", result.Errors.Select(e => e.Description)));
                    return;
                }


                await userManager.AddToRolesAsync(admin, roles: ["Admin", "Worker", "Patient"]);
                logger.LogInformation("Admin user created successfully.");

            }
        }
    }
}
