using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MOROPO.FCBC.SGI.Options;
using Serilog;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Extensions;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Security.Extensions;
using SmartSolucionesCuba.SAPRESSC.Core.Web.SecurityLocal.Extensions;
using SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Web.Common.Extensions;
using SmartSolucionesCuba.SAPRESSC.Middleware.Media.Web.Common.Extensions;
using System.Linq;

namespace MOROPO.FCBC.SGI
{
    public class Startup
    {
        private readonly IConfiguration configuration;
        private readonly IHostingEnvironment env;

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();

            this.configuration = configuration;
            this.env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<MediaThumbnailResizeOptions>(configuration.GetSection("MediaThumbnailResize"));
            services.Configure<JwtOptions>(configuration.GetSection("JWT"));

            services.AddDbContext<Persistence.EF.ApplicationDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<DbContext, Persistence.EF.ApplicationDbContext>();

            services.PerformCoreWebSecurityServiceRegistration();
            services.PerformCoreWebSecurityLocalServiceRegistration();
            services.PerformMiddlewareMediaWebCommonServiceRegistration();
            services.PerformMiddlewareIdentityWebCommonServiceRegistration();

            services.AddScoped<IEntityRepository<SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Entities.Trace, long>, SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Repositories.TraceRepository>();

            services.AddSingleton<SmartSolucionesCuba.SAPRESSC.Middleware.Media.Web.Common.Managers.Facade.IImageFileManagerFacade, Managers.RelativeImageFileManager>();
            services.AddSingleton<SmartSolucionesCuba.SAPRESSC.Middleware.Media.Web.Common.Managers.Facade.IThumbnailFileManagerFacade, Managers.RelativeThumbnailFileManager>();

            services.AddSingleton<SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Providers.Facade.IDbSchemaConfigurationProviderFacade, Persistence.EF.Providers.ApplicationDbSchemaConfigurationProvider>();

            services.AddSingleton<SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Helpers.Facade.IDataInitializationHelper, Helpers.ApplicationDataInitializationHelper>();

            services.AddAuthentication(config =>
            {
                config.DefaultScheme = Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(config =>
            {
                config.AccessDeniedPath = new PathString("/security/error/forbidden");
                config.LoginPath = new PathString("/security/account/login");
            })
            .AddJwtBearer(config =>
            {
                config.RequireHttpsMetadata = false;
                config.SaveToken = true;
                config.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidAudience = configuration["JWT:Issuer"],
                    ValidateLifetime = false,
                    RequireExpirationTime = false,
                    IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration["JWT:Key"])),
                    ClockSkew = System.TimeSpan.Zero
                };
            });

            #region SmartSolucionesCuba.SAPRESSC.Core.Security concreter dependencies configuration
            services.AddSingleton<SmartSolucionesCuba.SAPRESSC.Core.Web.Security.Licensing.ILicenseValidator, Security.Licensing.WilcardLicenseValidator>();

            services.AddTransient<SmartSolucionesCuba.SAPRESSC.Core.Web.Security.Models.View.IUserProfileViewModel, Models.View.UserProfileViewModel>();

            services.AddSingleton<SmartSolucionesCuba.SAPRESSC.Core.Web.Common.ModelBinding.IConcretModelBindHelper, Helpers.Binding.ConcretModelBindHelper>();

            services.AddScoped<SmartSolucionesCuba.SAPRESSC.Core.Web.Security.Providers.Facade.IIdentityProviderFacade, SmartSolucionesCuba.SAPRESSC.Core.Web.SecurityLocal.Managers.IdentityManager>();
            services.AddScoped<SmartSolucionesCuba.SAPRESSC.Core.Web.Security.Managers.Facade.IUserProfileManagerFacade, Managers.UserProfileManager>();
            #endregion

            #region Globalization configuration
            var defaultCulture = new System.Globalization.CultureInfo("es");

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(defaultCulture);
                options.SupportedCultures = new[] { defaultCulture };
                options.SupportedUICultures = new[] { defaultCulture };

                options.RequestCultureProviders = new[] { new SmartSolucionesCuba.SAPRESSC.Core.Web.Common.Localization.UrlRequestCultureProvider() };
            });

            services.AddSingleton<Helpers.Localization.GlobalViewLocalizationHelper>();
            #endregion

            services.Configure<Microsoft.AspNetCore.Routing.RouteOptions>(options => options.LowercaseUrls = true);

            services.AddResponseCaching();
            services.AddMemoryCache();
            services.AddSession();

            services.AddMvc(mvc =>
                mvc.ModelBinderProviders.Insert(0, new SmartSolucionesCuba.SAPRESSC.Core.Web.Common.ModelBinding.AbstractsModelBinderProvider())
            )
            .AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix)
            .AddDataAnnotationsLocalization(options =>
                options.DataAnnotationLocalizerProvider = (type, factory) =>
                {
                    return factory.Create(typeof(SharedResources));
                }
            )
            .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_0);

            services.AddDataProtection().PersistKeysToFileSystem(new System.IO.DirectoryInfo(System.IO.Path.Combine(env.ContentRootPath, "dpkeys")));

            services.AddSingleton<Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataProvider, Microsoft.AspNetCore.Mvc.ViewFeatures.CookieTempDataProvider>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, Microsoft.Extensions.Options.IOptions<RequestLocalizationOptions> requestLocalizationOptions, Microsoft.Extensions.Logging.ILoggerFactory loggerFactory, DbContext dbContext)
        {
            loggerFactory.AddSerilog(Log.Logger);

            app.UseRequestLocalization(requestLocalizationOptions.Value);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else if (env.IsStaging())
            {
                dbContext.Database.Migrate();

                app.PerformDatabaseInitialization();

                if (!(dbContext as Persistence.EF.ApplicationDbContext).Users.Any())
                {
                    var defaultUserId = configuration["DefaultUser:Id"];

                    var k3y = new System.Guid(0x79c44606, 0xa758, 0x4393, 0x87, 0x72, 0xad, 0xa6, 0x7e, 0xe1, 0xbb, 0x3e);

                    var protectedPassword = (dbContext as Persistence.EF.ApplicationDbContext).CreateSecureUser(k3y, defaultUserId, "system");

                    Log.Logger.Information(string.Format(">>>>>> Protected Password : \"{0}\" => \"{1}\"", defaultUserId, protectedPassword));
                }
            }
            else
            {
                app.UseExceptionHandler("/error");

                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute("defaultLocalizedAreaRoutes",
                    template: "{culture:regex(^[[a-z]]{{2}}(?:-[[A-Z]]{{2}})?$)}/{area:exists}/{controller=start}/{action=index}/{id?}");

                routes.MapRoute(
                    name: "defaultLocalizedRoutes",
                    template: "{culture:regex(^[[a-z]]{{2}}(?:-[[A-Z]]{{2}})?$)}/{controller=start}/{action=index}/{id?}");

                routes.MapRoute("defaultAreaRoutes",
                    template: "{area:exists}/{controller=start}/{action=index}/{id?}");

                routes.MapRoute(
                    name: "defaultRoutes",
                    template: "{controller=start}/{action=index}/{id?}");
            });
        }
    }
}
