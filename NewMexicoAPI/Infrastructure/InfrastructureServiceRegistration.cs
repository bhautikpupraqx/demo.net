using NewMexicoAPI.Repositories;

namespace NewMexicoAPI.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ValidationErrorRepository>();
            services.AddScoped<RuleExplorerRepository>();
            services.AddScoped<StateHomeRepository>();
            services.AddScoped<ReportingPeriodRepository>();
            services.AddScoped<DashBoardCategoriesRepository>();
            services.AddScoped<DistrictHomeRepository>();
            services.AddScoped<SchoolHomeRepository>();
            return services;
        }
    }
}
