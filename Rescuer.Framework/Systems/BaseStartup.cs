using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Rescuer.Framework.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rescuer.Core.IoC;

namespace Rescuer.Framework.Systems
{
    public class BaseStartup
    {
        public BaseStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected IConfiguration Configuration { get; set; }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddJsonOptionsConfig();

            services.AddCors();

            services.AddApiVersioningConfig(Configuration);

            services.AddHttpContextAccessor();

            services.AddSwaggerGenConfig();

            //services.RegisterAll<IBaseRegisterType>();
        }

        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.Use(async (context, next) =>
                {
                    if (context.Request.IsLocal())
                    {
                        // Forbidden http status code
                        context.Response.StatusCode = 403;
                        return;
                    }

                    await next.Invoke();
                });
            }


            app.UseSwaggerUIConfig();

            app.UseRouting();

            app.UseCorsConfig();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}