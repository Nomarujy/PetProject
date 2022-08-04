using Portfolio.Areas._7DTD.Data.BloodNightRepository;

namespace Portfolio.Areas
{
    public static class AreaServicesRegsiter
    {
        public static void AddAreaServices(this IServiceCollection services)
        {
            services.AddSingleton<IBloodNightRepository, BloodNightRepository>();
        }
    }
}
