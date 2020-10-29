using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Rescuer.Identity.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            // using (var scope = host.Services.CreateScope())
            // {
            //     var services = scope.ServiceProvider;
            //     var appDbContext = services.GetRequiredService<NpgsqlContext>();
            //     await DbSeeder.Seed(appDbContext);
            // }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
//https://erhanballieker.com/2018/06/26/asp-net-core-2-0a-giris-bolum-2/