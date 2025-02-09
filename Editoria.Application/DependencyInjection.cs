using Course_Work_Editoria.Services.Auth;
using Editoria.Application.Services.Implementation;
using Editoria.Application.Services.Interfaces;
using Editoria.Application.Services.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editoria.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IAdvertisementService, AdvertisementService>();
            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IEditorService, EditorService>();
            services.AddScoped<IIssueService, IssueService>();
            services.AddScoped<INewspaperService, NewspaperService>();
            services.AddScoped<ITagService, TagService>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProfileService, ProfileService>();

            services.AddScoped<DropdownDataService>();


            return services;
        }
    }
}

