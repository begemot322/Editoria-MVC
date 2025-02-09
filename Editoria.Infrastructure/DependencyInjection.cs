using Course_Work_Editoria.Services.File;
using Editoria.Application.Common.Interfaces.Identity;
using Editoria.Application.Common.Interfaces.Repositories;
using Editoria.Application.Common.Interfaces.Services;
using Editoria.Domain.Constants;
using Editoria.Infrastructure.Data;
using Editoria.Infrastructure.Identity.Auth;
using Editoria.Infrastructure.Identity.Password;
using Editoria.Infrastructure.Repository;
using Editoria.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Editoria.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDatabase(configuration);
            services.AddRepositories();
            services.AddServices();
            services.AddAuth(configuration);

            return services;
        }

        private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connString));
            services.AddScoped<DbInitializer>();

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAdvertisementRepository, AdvertisementRepository>();
            services.AddScoped<IArticleRepository, ArticleRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IEditorRepository, EditorRepository>();
            services.AddScoped<IIssueRepository, IssueRepository>();
            services.AddScoped<INewspaperRepository, NewspaperRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITagRepository, TagRepository>();

            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();

            services.AddMemoryCache();

            return services;
        }
        private static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
            services.AddScoped<IJwtProvider, JwtProvider>();

            var jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtOptions.Issuer,
                        ValidateAudience = true,
                        ValidAudience = jwtOptions.Audience,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions!.SecretKey)),
                        RoleClaimType = ClaimTypes.Role,
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies["SecurityCookies"];
                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(Policies.AdminPolicy, policy =>
                     policy.RequireRole(Roles.Admin));

                options.AddPolicy(Policies.ModeratorPolicy, policy =>
                    policy.RequireRole(Roles.Admin, Roles.Moderator));

                options.AddPolicy(Policies.UserPolicy, policy =>
                    policy.RequireRole(Roles.User, Roles.Admin, Roles.Moderator));

                options.AddPolicy(Policies.GuestPolicy, policy =>
                    policy.RequireAssertion(context => !context.User.Identity.IsAuthenticated));
            });

            return services;
        }
    }
}