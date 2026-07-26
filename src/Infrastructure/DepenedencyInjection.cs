

using Application.Common.Interfaces;
using Ardalis.GuardClauses;
using Domain.Constants;
using Infrastructure.Data;
using Infrastructure.Data.Interceptors;
using Infrastructure.Extensions;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        Guard.Against.Null(connectionString, message: "Connection string 'DefaultConnection' not found.");

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.ConfigureRepositeries();
        services.ConfigureServices();

        // TODO
        //services.AddScoped<IIncomeTaxCalculator, IncomeTaxCalculator>();

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseSqlServer(connectionString);

#if (UseSQLite)
            options.UseSqlite(connectionString);
#else
#endif
        }, ServiceLifetime.Scoped);

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<ApplicationDbContextInitialiser>();

        #region working
        services.AddAuthentication()
            .AddBearerToken(IdentityConstants.BearerScheme);


        services.AddAuthorizationBuilder();

        //services
        //    .AddIdentityCore<ApplicationUser>()
        //    .AddRoles<IdentityRole>()
        //    .AddEntityFrameworkStores<ApplicationDbContext>()
        //    .AddApiEndpoints();

        //services.AddIdentity<ApplicationUser, IdentityRole>()
        //.AddEntityFrameworkStores<ApplicationDbContext>()
        //.AddDefaultTokenProviders();
        #endregion

        #region try and run code

        // For Identity
        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        // Adding Authentication
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })

        // Adding Jwt Bearer
        .AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                //ValidAudience = Configuration["JWT:ValidAudience"],
                //ValidIssuer = Configuration["JWT:ValidIssuer"],
                //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                ValidAudience = configuration["JWT:ValidAudience"],
                ValidIssuer = configuration["JWT:ValidIssuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
            };
        });




        #endregion


        services.AddSingleton(TimeProvider.System);
        //services.AddTransient<IIdentityService, IdentityService>();

        services.AddAuthorization(options =>
            options.AddPolicy(Policies.CanPurge, policy => policy.RequireRole(Roles.Administrator)));

        services.AddAuthorization(options =>
        {
            //options.AddPolicy("HRManagerPolicy", policy =>
            //    policy.RequireRole("SuperAdmin", "Administrator", "HRManager")
            //            .RequireClaim("HRManagerClaim"));

            options.AddPolicy(Policies.AdministratorPolicy, policy =>
                policy.RequireRole(Roles.Administrator, Roles.SuperAdmin, Roles.User));

            //options.AddPolicy(Policies.SuperAdminPolicy, policy =>
            //    policy.RequireRole(Roles.Administrator, Roles.HrManager));

            //options.AddPolicy(Policies.HRManagerAndEmployeePolicy, policy =>
            //    policy.RequireRole(Roles.HrManager, Roles.Employee));

            //options.AddPolicy(Policies.EmployeePolicy, policy =>
            //    policy.RequireRole(Roles.Employee));
        });

        return services;
    }
}
