using FluentValidation.AspNetCore;
using FluentValidation;

namespace HoTeach.API.Infrastructure.Validation
{
    public static class FluentValidationServiceCollectionExtensions
    {
        /// <summary>
        /// Adds FluentValidation to the application's service collection.
        /// </summary>
        /// <param name="services">The IServiceCollection instance.</param>
        /// <returns>The IServiceCollection instance for chaining.</returns>
        public static IServiceCollection AddFluentValidation(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<Program>(); 
            services.AddFluentValidationAutoValidation();           
            services.AddFluentValidationClientsideAdapters();       

            return services;
        }
    }
}
