namespace Editoria.Web
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddHttpContextAccessor();

            return services;
        }

    }
}
