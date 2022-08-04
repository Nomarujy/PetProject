using Portfolio.Utilites.Logger.Json;

namespace Portfolio.Utilites
{
    public static class AddUtilitesService
    {
        public static void AddLogerProviders(this WebApplicationBuilder builder)
        {
            if (builder.Environment.IsDevelopment()) return;

            builder.Logging.AddProvider(new JsonLoggerProvider());
        }
    }
}
