using Editoria.Data.Context;
using Editoria.Data.Repository.IRepository;
using Editoria.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Course_Work_Editoria.Authentication.Interfaces;
using Course_Work_Editoria.Authentication;
using Course_Work_Editoria.Authentication.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;

namespace Course_Work_Editoria.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static WebApplicationBuilder AddDatabaseServices(this WebApplicationBuilder builder)
        {
            var connString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connString));

            return builder;
        }

        public static WebApplicationBuilder AddRepositoryServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IAdvertisementRepository, AdvertisementRepository>();
            builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
            builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IEditorRepository, EditorRepository>();
            builder.Services.AddScoped<IIssueRepository, IssueRepository>();
            builder.Services.AddScoped<INewspaperRepository, NewspaperRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ITagRepository, TagRepository>();

            return builder;
        }
        public static WebApplicationBuilder AddJwtServices(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));
            builder.Services.AddScoped<IJwtProvider, JwtProvider>();

            return builder;
        }
        public static WebApplicationBuilder AddApplicationServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

            builder.Services.AddHttpContextAccessor();

            return builder;
        }
        public static WebApplicationBuilder AddAuthenticationServices(this WebApplicationBuilder builder)
        {
            var configuration = builder.Configuration;
            var jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();

            builder.Services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
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

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminPolicy", policy =>
                    policy.RequireRole("Admin"));

                options.AddPolicy("ModeratorPolicy", policy =>
                    policy.RequireRole("Admin", "Moderator"));

                options.AddPolicy("UserPolicy", policy =>
                    policy.RequireRole("User", "Admin", "Moderator"));

                options.AddPolicy("GuestPolicy", policy => policy.RequireAssertion(context =>
                    !context.User.Identity.IsAuthenticated));  
            });

            return builder;

        }
    }
}
