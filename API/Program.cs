using API.Core.ServiceConfig;
using Application.Core.Filters;
using Domain.Models.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Persistence.Database;
using Persistence.Seed;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(opt=>opt.Filters.Add<ValidationFilter>());

builder.Services.AddDbContext<BeeCodeDbContext>(opt =>
    opt.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Database=BeeCode;Integrated Security=True;TrustServerCertificate=True;")
);
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000");
    });
});
builder.Services.AddIdentity<User, IdentityRole<Guid>>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<BeeCodeDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
    opt.AddPolicy("Patient", policy => policy.RequireRole("Patient"));
    opt.AddPolicy("Worker", policy => policy.RequireRole("Worker"));
});


var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var key = jwtSettings["Key"];

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.AutomaticRefreshInterval = TimeSpan.FromDays(10);
    options.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
    };
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            Console.WriteLine($"Authentication failed: {context.Exception.Message}");
            return Task.CompletedTask;
        },
        OnChallenge = context =>
        {
            Console.WriteLine($"Challenge issued with error: {context.Error}, description: {context.ErrorDescription}");
            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            var token = context.Request.Headers["Authorization"]
            .ToString()
            .Replace("Bearer ", "");

            var handler = new JwtSecurityTokenHandler();

            if (handler.CanReadToken(token))
            {
                var jwt = handler.ReadJwtToken(token);

                Console.WriteLine($"Token validated: {jwt.RawData}");
                Console.WriteLine($"Claims: {string.Join(", ", jwt.Claims.Select(c => $"{c.Type}: {c.Value}"))}");
                Console.WriteLine($"Valid To (UTC): {jwt.ValidTo}");
            }
            else
            {
                Console.WriteLine("Cannot read token");
            }
            return Task.CompletedTask;
        }
    };
});

ServiceConfig.ConfigureServices(builder.Services);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

    string[] roleNames = { "Admin", "Patient", "Worker" };
    foreach (var roleName in roleNames)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole<Guid>(roleName));
        }
    }
    await DbSeed.SeedDb(scope.ServiceProvider);
}

app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseStaticFiles();
app.Run();
