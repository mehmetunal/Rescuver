using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Rescuer.Framework.Extensions
{
    public static class ApiVersioningExtensions
    {
        /// <summary>
        /// AddApiVersioningConfig
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddApiVersioningConfig(this IServiceCollection services,
            IConfiguration configuration)
        {
            var majorVersion = 1;
            var minorVersion = 1;

            var majorVersionConfig = configuration.GetSection("ApiVersion:MajorVersion")?.Value;
            var minorVersionConfig = configuration.GetSection("ApiVersion:MinorVersion")?.Value;

            if (!string.IsNullOrEmpty(majorVersionConfig))
                majorVersion = int.Parse(majorVersionConfig);

            if (!string.IsNullOrEmpty(minorVersionConfig))
                minorVersion = int.Parse(minorVersionConfig);

            services.AddApiVersioning(option =>
            {
                option.DefaultApiVersion = new ApiVersion(majorVersion, minorVersion);
                option.AssumeDefaultVersionWhenUnspecified = true;
                option.ReportApiVersions = true;
                option.ApiVersionReader = ApiVersionReader.Combine(new HeaderApiVersionReader("x-api-version"),
                    new QueryStringApiVersionReader("api-version"));
            });
            return services;
        }
    }
}