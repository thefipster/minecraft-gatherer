using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleInjector;
using TheFipster.Minecraft.Speedrun.Modules;
using TheFipster.Minecraft.Speedrun.Services;
using TheFipster.Minecraft.Speedrun.Web.Enhancer;

namespace TheFipster.Minecraft.Speedrun.Web
{
    public class Startup
    {
        private Container _container;

        public Startup(IConfiguration configuration)
        {
            _container = new Container();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddSimpleInjector(_container, options =>
                options.AddAspNetCore()
                       .AddControllerActivation());

            InitializeContainer();
        }

        private void InitializeContainer()
        {
            _container.Register<IConfigService, ConfigService>(Lifestyle.Singleton);
            _container.Register<IPlayerStore, PlayerConfigStore>(Lifestyle.Singleton);
            _container.Register<IRunStore, RunLiteStore>(Lifestyle.Singleton);

            _container.Register<IStatsFinder, StatsFinder>(Lifestyle.Transient);
            _container.Register<IWorldFinder, WorldFinder>(Lifestyle.Transient);
            _container.Register<ILogFinder, LogFinder>(Lifestyle.Transient);
            _container.Register<IRunFinder, RunFinder>(Lifestyle.Transient);

            _container.Register<IWorldLoader, WorldLoader>(Lifestyle.Transient);
            _container.RegisterDecorator<IWorldLoader, WorldLoadVerifier>(Lifestyle.Transient);
            _container.RegisterDecorator<IWorldLoader, WorldDimensionLoader>(Lifestyle.Transient);

            _container.Register<ILogParser, LogParser>(Lifestyle.Transient);
            _container.Register<ILogTrimmer, LogTrimmer>(Lifestyle.Transient);
            _container.Register<ILogEventExtractor, LogEventExtractor>(Lifestyle.Transient);
            _container.RegisterDecorator<ILogEventExtractor, LogLineAnalyzer>(Lifestyle.Transient);

            _container.Register<ILineAnalyzer, LineAnalyzer>(Lifestyle.Transient);
            _container.RegisterDecorator<ILineAnalyzer, LineAdvancementAnalyzer>(Lifestyle.Transient);
            _container.RegisterDecorator<ILineAnalyzer, LineSetTimeAnalyzer>(Lifestyle.Transient);
            _container.RegisterDecorator<ILineAnalyzer, LinePlayerJoinAnalyzer>(Lifestyle.Transient);
            _container.RegisterDecorator<ILineAnalyzer, LinePlayerLeaveAnalyzer>(Lifestyle.Transient);

            _container.Register<IEventPlayerExtractor, EventPlayerExtractor>(Lifestyle.Transient);
            _container.Register<IStatsPlayerExtractor, StatsPlayerExtractor>(Lifestyle.Transient);
            _container.Register<IEventTimingExtractor, EventTimingExtractor>(Lifestyle.Transient);
            _container.Register<IStatsExtractor, StatsExtractor>(Lifestyle.Transient);
            _container.Register<IAchievementEventExtractor, AchievementEventExtractor>(Lifestyle.Transient);

            _container.Register<IValidityChecker, ValidityChecker>(Lifestyle.Transient);
            _container.Register<IOutcomeChecker, OutcomeChecker>(Lifestyle.Transient);

            _container.Register<IServerPropertiesReader, ServerPropertiesReader>(Lifestyle.Transient);

            _container.Register<IImportModule, ImportModule>(Lifestyle.Transient);

            _container.Register<IQuickestEventEnhancer, QuickestEventEnhancer>(Lifestyle.Transient);
            _container.Register<IPlayerEventEnhancer, PlayerEventEnhancer>(Lifestyle.Transient);
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

            app.UsePathBase(Configuration["BasePath"]);
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
