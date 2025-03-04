using Editoria.Web.Services;

namespace Editoria.Web
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddHttpContextAccessor();

            services.AddScoped<DropdownDataService>();

            return services;
        }

    }
}
