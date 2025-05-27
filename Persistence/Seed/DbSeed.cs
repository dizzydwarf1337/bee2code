using Domain.Enums.LabTesting;
using Domain.Models.LabTesting;
using Domain.Models.Links;
using Domain.Models.Researches;
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
                if (await context.Users.AnyAsync())
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


                await userManager.AddToRoleAsync(admin,"Admin");
                logger.LogInformation("Admin user created successfully.");

                logger.LogInformation("Creating worker1 user...");
                var worker1 = new User
                {
                    Email = "worker1@gmail.com",
                    LastName = "John",
                    FirstName = "Doe",
                    EmailConfirmed = true,
                    City = "Warszawa",
                    Country = "Poland",
                    Street = "Piekna",
                    HouseNumber = "24",
                    PostalCode = "12881",
                    PhoneNumber = "+48123456789"
                };
                worker1.UserName = worker1.Email;
                var worker1Result = await userManager.CreateAsync(worker1, "WorkerPassword1!");

                if (!worker1Result.Succeeded)
                {
                    logger.LogError("Failed to create worker 1: {Errors}", string.Join(", ", result.Errors.Select(e => e.Description)));
                    return;
                }


                await userManager.AddToRoleAsync(worker1, "Worker");
                logger.LogInformation("Worker1 user created successfully.");
               
                logger.LogInformation("Creating worker2 user...");
                var worker2 = new User
                {
                    Email = "worker2@gmail.com",
                    LastName = "Jane",
                    FirstName = "Doe",
                    EmailConfirmed = true,
                    City = "Bialystok",
                    Country = "Poland",
                    Street = "Przydworcowa",
                    HouseNumber = "12",
                    PostalCode = "12-123",
                    PhoneNumber = "+48123465789"
                };
                worker2.UserName = worker2.Email;
                var worker2Result = await userManager.CreateAsync(worker2, "WorkerPassword1!");

                if (!worker2Result.Succeeded)
                {
                    logger.LogError("Failed to create worker 2: {Errors}", string.Join(", ", result.Errors.Select(e => e.Description)));
                    return;
                }


                await userManager.AddToRoleAsync(worker2, "Worker");
                logger.LogInformation("Worker2 user created successfully.");




                logger.LogInformation("Creating Patient1 user...");
                var patient1 = new User
                {
                    Email = "patient1@gmail.com",
                    LastName = "Smith",
                    FirstName = "Emily",
                    EmailConfirmed = true,
                    City = "Gdansk",
                    Country = "Poland",
                    Street = "Dluga",
                    HouseNumber = "8A",
                    PostalCode = "80-831",
                    PhoneNumber = "+48502345678"
                };
                patient1.UserName = patient1.Email;
                var patient1Result = await userManager.CreateAsync(patient1, "PatientPassword1!");

                if (!patient1Result.Succeeded)
                {
                    logger.LogError("Failed to create patient 1: {Errors}", string.Join(", ", result.Errors.Select(e => e.Description)));
                    return;
                }


                await userManager.AddToRoleAsync(patient1, "Patient");
                logger.LogInformation("Patient1 user created successfully.");




                logger.LogInformation("Creating Patient2 user...");
                var patient2 = new User
                {
                    Email = "patient2@gmail.com",
                    LastName = "Nowak",
                    FirstName = "Anna",
                    EmailConfirmed = true,
                    City = "Krakow",
                    Country = "Poland",
                    Street = "Karmelicka",
                    HouseNumber = "15",
                    PostalCode = "31-133",
                    PhoneNumber = "+48765432109"
                };
                patient2.UserName = patient2.Email;
                var patient2Result = await userManager.CreateAsync(patient2, "PatientPassword1!");

                if (!patient2Result.Succeeded)
                {
                    logger.LogError("Failed to create patient 2: {Errors}", string.Join(", ", result.Errors.Select(e => e.Description)));
                    return;
                }


                await userManager.AddToRoleAsync(patient2, "Patient");
                logger.LogInformation("Patient2 user created successfully.");





                logger.LogInformation("Adding researches");

                var research1 = new Research
                {
                    Name = "COVID-19 Vaccine Study",
                    Description = "A study to evaluate the effectiveness of the COVID-19 vaccine.",
                    OwnerId = worker1.Id
                };
                context.Researches.Add(research1);
                await context.SaveChangesAsync();
                
                
                var UserResearch = new UserResearch
                {
                    UserId = patient1.Id,
                    ResearchId = research1.Id
                };
                
                research1.Patients.Add(UserResearch);
                await context.SaveChangesAsync();


                var labtest1 = new LabTest
                {
                    Name = "COVID-19 Antibody Test",
                    Description = "A test to check for antibodies against the COVID-19 virus.",
                    LabTestType = LabTestType.ImmunologyTesting,
                    Date = DateTime.Now.AddDays(-1),
                    CreatorId = worker1.Id,
                    PatientId = patient1.Id,
                    ResearchId = research1.Id
                };


                await context.LabTests.AddAsync(labtest1);
                await context.SaveChangesAsync();

                var labtestresult1 = new LabTestResult
                {
                    LabTestId = labtest1.Id,
                    Result = "IgG: 140 AU/mL, IgM: 5 AU/mL – Positive (IgG positive, IgM negative)",
                };
                await context.LabTestResults.AddAsync(labtestresult1);
                labtest1.LabTestResult = labtestresult1;
                labtest1.LabTestResultId = labtestresult1.Id;
                context.LabTests.Update(labtest1);

                await context.SaveChangesAsync();


                var research2 = new Research
                {
                    Name = "Diabetes Prevention Program",
                    Description = "A long-term study focused on identifying lifestyle factors that reduce the risk of developing type 2 diabetes.",
                    OwnerId = worker2.Id
                };
                context.Researches.Add(research2);
                await context.SaveChangesAsync();


                var UserResearch2 = new UserResearch
                {
                    UserId = patient2.Id,
                    ResearchId = research2.Id
                };

                research2.Patients.Add(UserResearch2);
                await context.SaveChangesAsync();


                var labtest2 = new LabTest
                {
                    Name = "Fasting Blood Glucose Test",
                    Description = "A test to measure blood sugar levels after fasting, used to assess risk of developing type 2 diabetes.",
                    LabTestType = LabTestType.BloodTest,
                    Date = DateTime.Now.AddDays(5),
                    CreatorId = worker2.Id,
                    PatientId = patient2.Id,
                    ResearchId = research2.Id
                };


                await context.LabTests.AddAsync(labtest2);
                await context.SaveChangesAsync();


            }
        }
    }
}
