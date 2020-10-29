using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Rescuer.Framework.Extensions
{
    public static class NpgsqlExtensions
    {
        public static IServiceCollection AddNpgsqlConfig<TContext>(this IServiceCollection services,
            IConfiguration configuration) where TContext : DbContext
        {
            var connection = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<TContext>(options => { options.UseNpgsql(connection); });
            return services;
        }
    }
}