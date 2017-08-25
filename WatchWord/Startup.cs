using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using WatchWord.Service;
using WatchWord.Service.Abstract;
using WatchWord.DataAccess.Repositories;
using WatchWord.Infrastructure;
using WatchWord.DataAccess;
using WatchWord.Service.Infrastructure;
using Microsoft.AspNetCore.Identity;
using WatchWord.DataAccess.Abstract;
using WatchWord.DataAccess.Identity;
using System.Diagnostics;

namespace WatchWord
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        private void ConfigureDependencyInjection(IServiceCollection services)
        {
            // Configuration
            services.AddSingleton<IConfiguration>(Configuration);

            // Entity Framework
            services.AddScoped(typeof(DbContext), typeof(WatchWordContext));

            // DataAccess
            services.AddSingleton<DatabaseSettings, DatabaseSettings>();
            services.AddScoped<IWordsRepository, WordsRepository>();
            services.AddScoped<ISettingsRepository, SettingsRepository>();
            services.AddScoped<IAccountsRepository, AccountsRepository>();
            services.AddScoped<IMaterialsRepository, MaterialsRepository>();
            services.AddScoped<IVocabWordsRepository, VocabWordsRepository>();
            services.AddScoped<ITranslationsRepository, TranslationsRepository>();
            services.AddScoped<IWatchWordUnitOfWork, WatchWordUnitOfWork>();

            // Services
            services.AddScoped<IPictureService, PictureService>();
            services.AddScoped<IScanWordParser, ScanWordParser>();
            services.AddScoped<ISettingsService, SettingsService>();
            services.AddScoped<IAccountsService, AccountsService>();
            services.AddScoped<IMaterialsService, MaterialsService>();
            services.AddScoped<IVocabularyService, VocabularyService>();
            services.AddScoped<ITranslationService, TranslationService>();

            // Development
            services.AddSingleton<WatchWordProxy, WatchWordProxy>();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Options
            services.AddOptions();

            // Disable CORS: Only for Dev
            var corsEnabled = Configuration.GetValue<bool>("Config:EnableCors");
            if (!corsEnabled)
            {
                services.AddCors(options =>
                {
                    options.AddPolicy("CorsPolicy",
                        builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
                });
            }

            // Dependency injection
            ConfigureDependencyInjection(services);

            // Database context for identity
            services.AddDbContext<WatchWordContext>();

            // Idenity
            services.AddIdentity<WatchWordUser, WatchWordRole>()
                .AddEntityFrameworkStores<WatchWordContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromDays(30);

                // CORS: Only for Dev
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;

                // 401 error handler
                options.Events = new CookieAuthenticationEvents
                {
                    OnRedirectToLogin = ctx =>
                    {
                        if (ctx.Request.Path.StartsWithSegments("/api") &&
                            ctx.Response.StatusCode == (int)HttpStatusCode.OK)
                        {
                            ctx.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        }
                        else
                        {
                            ctx.Response.Redirect(ctx.RedirectUri);
                        }
                        return Task.FromResult(0);
                    }
                };
            });

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;

                // User settings
                options.User.RequireUniqueEmail = true;
            });

            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, RoleManager<WatchWordRole> roleManager)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            // Disable CORS: Only for Dev
            var corsEnabled = Configuration.GetValue<bool>("Config:EnableCors");
            if (!corsEnabled)
            {
                app.UseCors("CorsPolicy");
            }

            app.UseDefaultFiles();
            // Angular 2 PathRoutingStategy
            app.UseMiddleware<Angular2Middleware>();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc();

            // Add identity roles
            CreateRoles(roleManager);
        }

        private void CreateRoles(RoleManager<WatchWordRole> roleManager)
        {
            string[] roleNames = { "Admin", "Member" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = roleManager.RoleExistsAsync(roleName).Result;
                if (!roleExist)
                {
                    roleResult = roleManager.CreateAsync(new WatchWordRole(roleName)).Result;
                    if (roleResult.Succeeded == false)
                    {
                        foreach (var er in roleResult.Errors)
                        {
                            Trace.TraceError(er.ToString());
                        }
                    }
                }
            }
        }
    }
}