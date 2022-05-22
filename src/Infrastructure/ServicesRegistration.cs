using System.Text;
using Application.Contracts.Authorization;
using Domain.Common;
using Domain.Common.Repositories;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Services;
using Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure;

public static class ServicesRegistration
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ITokenFactory, JwtTokenFactory>();
        services.AddScoped<IRefreshTokenValidator, RefreshTokenValidator>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IAuthorizationService, AuthorizationService>();
        services.AddScoped<IAccountService, AccountService>();

        //var connectionString = "Server=ec2-3-217-219-146.compute-1.amazonaws.com;Port=5432;Database=daqt4ghivqrea3;Username=tyutubixvpegee;Password=420000d402bb54e446c4a08351b4221310520de112aba9e4ddde478d6d6319fb";
        var connectionString = configuration.GetConnectionString("default");
        services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString)
                                                                      .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
        services.AddIdentity<IdentityUser, IdentityRole>()
                .AddErrorDescriber<CustomIdentityErrorDescriber>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
        
        services.AddJwtSettings(configuration, out var jwtSettings);
        services.AddJwtTokenAuthentication(jwtSettings);

        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
        services.AddTransient<IEventRepository, EventRepository>();
        services.AddTransient<IClientRepository, ClientRepository>();

        return services;
    }

    private static IServiceCollection AddJwtSettings(this IServiceCollection services, IConfiguration configuration, out JwtSettings jwtSettings)
    {
        jwtSettings = new JwtSettings();
        configuration.Bind(nameof(JwtSettings), jwtSettings);

        services.AddSingleton(jwtSettings);
        
        return services;
    }
    
    private static IServiceCollection AddJwtTokenAuthentication(this IServiceCollection services, JwtSettings jwtSettings)
    {
        services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.AccessTokenSecret)),
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ClockSkew = TimeSpan.Zero
                    };
                });

        return services;
    }
}