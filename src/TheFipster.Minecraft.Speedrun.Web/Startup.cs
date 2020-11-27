using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleInjector;
using TheFipster.Minecraft.Speedrun.Modules;
using TheFipster.Minecraft.Speedrun.Services;
using TheFipster.Minecraft.Speedrun.Web.Dependencies;
using TheFipster.Minecraft.Speedrun.Web.Enhancer;

namespace TheFipster.Minecraft.Speedrun.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Container = new Container();
        }

        public IConfiguration Configuration { get; }
        public Container Container { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddSimpleInjector(Container, options =>
                options.AddAspNetCore()
                       .AddControllerActivation());

            InitializeContainer();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSimpleInjector(Container);

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

            Container.Verify();
        }

        private void InitializeContainer()
        {
            Container.Register<IConfigService, ConfigService>(Lifestyle.Singleton);
            Container.Register<IPlayerStore, PlayerConfigStore>(Lifestyle.Singleton);

            Container.Register<IDatabaseHandler, LiteDatabaseHandler>(Lifestyle.Singleton);
            Container.Register<IRunStore, RunLiteStore>(Lifestyle.Transient);
            Container.Register<ITimingStore, TimingLiteStore>(Lifestyle.Transient);

            Container.Register<IStatsFinder, StatsFinder>(Lifestyle.Transient);
            Container.Register<IWorldFinder, WorldFinder>(Lifestyle.Transient);
            Container.Register<ILogFinder, LogFinder>(Lifestyle.Transient);
            Container.Register<IRunFinder, RunFinder>(Lifestyle.Transient);

            Container.Register<IWorldLoader, WorldLoader>(Lifestyle.Transient);
            Container.RegisterDecorator<IWorldLoader, WorldLoadVerifier>(Lifestyle.Transient);
            Container.RegisterDecorator<IWorldLoader, WorldDimensionLoader>(Lifestyle.Transient);

            Container.Register<ILogParser, LogParser>(Lifestyle.Transient);
            Container.Register<ILogTrimmer, LogTrimmer>(Lifestyle.Transient);
            Container.Register<ILogEventExtractor, LogEventExtractor>(Lifestyle.Transient);
            Container.RegisterDecorator<ILogEventExtractor, LogLineAnalyzer>(Lifestyle.Transient);

            Container.Register<ILineAnalyzer, LineAnalyzer>(Lifestyle.Transient);
            Container.RegisterDecorator<ILineAnalyzer, LineAdvancementAnalyzer>(Lifestyle.Transient);
            Container.RegisterDecorator<ILineAnalyzer, LineSetTimeAnalyzer>(Lifestyle.Transient);
            Container.RegisterDecorator<ILineAnalyzer, LinePlayerJoinAnalyzer>(Lifestyle.Transient);
            Container.RegisterDecorator<ILineAnalyzer, LinePlayerLeaveAnalyzer>(Lifestyle.Transient);
            Container.RegisterDecorator<ILineAnalyzer, LineGameModeAnalyser>(Lifestyle.Transient);
            Container.RegisterDecorator<ILineAnalyzer, LineDeathAnalyzer>(Lifestyle.Transient);
            Container.RegisterDecorator<ILineAnalyzer, LineTeleportAnalyzer>(Lifestyle.Transient);

            Container.Register<IEventPlayerExtractor, EventPlayerExtractor>(Lifestyle.Transient);
            Container.Register<IStatsPlayerExtractor, StatsPlayerExtractor>(Lifestyle.Transient);
            Container.Register<IEventTimingExtractor, EventTimingExtractor>(Lifestyle.Transient);
            Container.Register<IStatsExtractor, StatsExtractor>(Lifestyle.Transient);
            Container.Register<IAchievementEventExtractor, AchievementEventExtractor>(Lifestyle.Transient);

            Container.Register<IValidityChecker, ValidityChecker>(Lifestyle.Transient);
            Container.Register<IOutcomeChecker, OutcomeChecker>(Lifestyle.Transient);

            Container.Register<IServerPropertiesReader, ServerPropertiesReader>(Lifestyle.Transient);

            Container.Register<IPlayerNbtReader, PlayerNbtReader>(Lifestyle.Transient);

            Container.Register<ITimingAnalyzer, TimingAnalyser>(Lifestyle.Transient);
            Container.RegisterDecorator<ITimingAnalyzer, TimingStartAnalyzer>(Lifestyle.Transient);
            Container.RegisterDecorator<ITimingAnalyzer, TimingFinishAnalyzer>(Lifestyle.Transient);
            Container.RegisterDecorator<ITimingAnalyzer, TimingPlaytimeAnalyzer>(Lifestyle.Transient);
            Container.RegisterDecorator<ITimingAnalyzer, TimingSpawnSectionAnalyzer>(Lifestyle.Transient);
            Container.RegisterDecorator<ITimingAnalyzer, TimingNetherSectionAnalyzer>(Lifestyle.Transient);
            Container.RegisterDecorator<ITimingAnalyzer, TimingFortressSectionAnalyzer>(Lifestyle.Transient);
            Container.RegisterDecorator<ITimingAnalyzer, TimingBlazeRodSectionAnalyzer>(Lifestyle.Transient);
            Container.RegisterDecorator<ITimingAnalyzer, TimingSearchSectionAnalyzer>(Lifestyle.Transient);
            Container.RegisterDecorator<ITimingAnalyzer, TimingStrongholdSectionAnalyzer>(Lifestyle.Transient);
            Container.RegisterDecorator<ITimingAnalyzer, TimingTheEndSectionAnalyzer>(Lifestyle.Transient);

            Container.Register<IWorldLoaderModule, WorldLoaderModule>(Lifestyle.Transient);
            Container.Register<IImportRunModule, ImportRunModule>(Lifestyle.Transient);
            Container.Register<IAnalyticsModule, AnalyticsModule>(Lifestyle.Transient);
            Container.Register<ISyncModule, SyncModule>(Lifestyle.Transient);

            Container.Register<IQuickestEventEnhancer, QuickestEventEnhancer>(Lifestyle.Transient);
            Container.Register<IPlayerEventEnhancer, PlayerEventEnhancer>(Lifestyle.Transient);
            Container.Register<IRunCounterEnhancer, RunCounterEnhancer>(Lifestyle.Transient);

            CommonDeps.Register(Container);
            ImportDeps.Register(Container);
            EnhancerDeps.Register(Container);
            AnalyticsDeps.Register(Container);
        }
    }
}
