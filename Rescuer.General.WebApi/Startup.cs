using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rescuer.Entites.Npgsql.Context;
using Rescuer.Framework.Extensions;
using Rescuer.Framework.Systems;
using Rescuer.General.WebApi.Services;
using Rescuer.Npgsql.Core.UnitOfWork;
using Rescuer.Npgsql.Core.UnitOfWork.Interface;

namespace Rescuer.General.WebApi
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

            services.AddNpgsqlConfig<NpgsqlContext>(Configuration);

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserService, UserService>();
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            base.Configure(app, env);
        }
    }
}