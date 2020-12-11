using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleInjector;
using TheFipster.Minecraft.HostedServices;
using TheFipster.Minecraft.Speedrun.Web.Dependencies;

namespace TheFipster.Minecraft.Speedrun.Web
{
    public class Startup
    {
        private Container _container;
        private IConfiguration _config;

        public Startup(IConfiguration configuration)
        {
            _config = configuration;
            _container = new Container();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddSimpleInjector(_container, options =>
                options.AddAspNetCore()
                       .AddControllerActivation());

            _container.RegisterCommon();
            _container.RegisterStorage();
            _container.RegisterImporter();
            _container.RegisterEnhancer();
            _container.RegisterExtender();
            _container.RegisterAnalytics();
            _container.RegisterManuals();
            _container.RegisterMeta();
            _container.RegisterWeb();

            services.AddHostedService<HelloWorldService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSimpleInjector(_container);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UsePathBase(_config["BasePath"]);
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            _container.Verify();
        }
    }
}
