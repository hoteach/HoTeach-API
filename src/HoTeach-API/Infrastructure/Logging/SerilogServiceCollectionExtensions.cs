using Serilog;

namespace HoTeach.API.Infrastructure.Logging
{
    public static class SerilogServiceCollectionExtensions
    {
        /// <summary>
        /// Configures Serilog as the logging provider for the application.
        /// </summary>
        /// <param name="services">The IServiceCollection to add logging services to.</param>
        /// <param name="configuration">The application configuration containing Serilog settings.</param>
        /// <returns>The IServiceCollection for chaining further configuration.</returns>
        /// <remarks>
        /// This method sets up Serilog using the provided configuration, enriches logs with context and environment information, 
        /// and disposes of the Serilog logger when the application shuts down.
        /// </remarks>
        public static IServiceCollection AddSerilogLogging(this IServiceCollection services, IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .Enrich.WithEnvironmentName()
                .CreateLogger();

            services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
            return services;
        }
    }
}