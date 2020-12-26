using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleInjector;
using TheFipster.Minecraft.Speedrun.Web.Dependencies;
using TheFipster.Minecraft.Speedrun.Web.Services;

namespace TheFipster.Minecraft.Speedrun.Web
{
    public class Startup
    {
        private readonly Container _container;
        private readonly IConfiguration _config;

        public Startup(IConfiguration configuration)
        {
            _config = configuration;
            _container = new Container();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options =>
                    {
                        options.LoginPath = "/account/login";
                        options.LogoutPath = "/account/logout";
                        options.Cookie.Name = "mc-watcher-auth-token";
                    });

            services.AddControllersWithViews();

            services.AddSimpleInjector(_container, options =>
            {
                _container.RegisterCommon();
                _container.RegisterStorage();
                _container.RegisterImporter();
                _container.RegisterEnhancer();
                _container.RegisterExtender();
                _container.RegisterAnalytics();
                _container.RegisterManuals();
                _container.RegisterMeta();
                _container.RegisterOverviewer();
                _container.RegisterWeb();

                options.AddHostedService<HelloWorldService>()
                       .AddHostedService<WorldRenderService>()
                       .AddAspNetCore()
                       .AddControllerActivation();
            });
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
            app.UseCookiePolicy(new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Strict
            });
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
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
