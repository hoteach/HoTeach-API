using FluentValidation.AspNetCore;
using HoTeach.API.Common;
using HoTeach.API.Common.Pagination.Header;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace HoTeach.API.Infrastructure.Web
{
    public static class WebServiceCollectionExtensions
    {
        public static IServiceCollection AddWeb(this IServiceCollection services, IConfiguration configuration)
            => services
                .AddWebComponents(configuration)
                .AddSwagger(configuration);


        private static IServiceCollection AddWebComponents(this IServiceCollection services, IConfiguration config)
        {
            services
                .AddMvc(o =>
                {
                    o.Filters.Add(new AuthorizeFilter(new AuthorizationPolicyBuilder().RequireAuthenticatedUser()
                        .Build()));

                    o.Filters.Add<PaginationHeadersFilter>();
                })
                .AddFluentValidation(validation => validation
                    .RegisterValidatorsFromAssemblyContaining<Result>())
                .AddNewtonsoftJson(opts =>
                {
                    opts.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            return services;
        }

        private static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HoTeach API", Version = "v1" });
            });
            return services;
        }
    }
}
