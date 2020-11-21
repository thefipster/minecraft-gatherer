using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;
using System.Threading.Tasks;

namespace TheFipster.Minecraft.Speedrun.Web
{
    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
            var config = createConfig();
            createLogger(config);

            try
            {
                Log.Information("Starting Host");
                await CreateHostBuilder(args).Build().RunAsync();
                Log.Information("Host Stopped");
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
            => Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(
                    webBuilder => webBuilder.UseStartup<Startup>());

        private static void createLogger(IConfiguration config)
            => Log.Logger = new LoggerConfiguration()
                .ReadFrom
                .Configuration(config)
                .CreateLogger();

        private static IConfigurationRoot createConfig()
            => new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
    }
}
