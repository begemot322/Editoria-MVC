using Editoria.Data.Context;
using Editoria.Data.Repository.IRepository;
using Editoria.Data.Repository;
using Microsoft.EntityFrameworkCore;

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

            return builder;
        }
    }
}
