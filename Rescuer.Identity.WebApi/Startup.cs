using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rescuer.Entites.Npgsql.Context;
using Rescuer.Framework.Extensions;
using Rescuer.Framework.Security.Token;
using Rescuer.Framework.Systems;
using Rescuer.Identity.WebApi.Services;
using Rescuer.Npgsql.Core.UnitOfWork;
using Rescuer.Npgsql.Core.UnitOfWork.Interface;

namespace Rescuer.Identity.WebApi
{
    public class Startup : BaseStartup
    {
        public Startup(IConfiguration configuration) : base(configuration)
        {
            Configuration = configuration;
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services.AddJwtConfig(Configuration);

            services.AddNpgsqlConfig<NpgsqlContext>(Configuration);

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IAccessTokenHandler, AccessTokenHandler>();
            services.AddScoped<IUsersServices, UsersServices>();
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<NpgsqlContext>();
                //context.Database.EnsureCreated();
                context.Database.Migrate();
            }

            base.Configure(app, env);
        }
    }
}