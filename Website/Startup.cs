using Asp2017.Server.Models;
using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using AspCoreServer.Data;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.AspNetCore.Http;
using DataBase;
using Microsoft.AspNetCore.Identity;
using Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace AspCoreServer
{
  public class Startup
  {

    public static void Main(string[] args)
    {
      var host = new WebHostBuilder()
          .UseKestrel()
          .UseContentRoot(Directory.GetCurrentDirectory())
          .UseIISIntegration()
          .UseStartup<Startup>()
          .UseUrls("http://localhost:5005/")
          .Build();
      OpenBrowser("http://localhost:5005/");
      host.Run();
    
    }
    public static void OpenBrowser(string url)
    {
      if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
      {
        Process.Start(new ProcessStartInfo("cmd", $"/c start {url}")); // Works ok on windows
      }
      else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
      {
        Process.Start("xdg-open", url);  // Works ok on linux
      }
      else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
      {
        Process.Start("open", url); // Not tested
      }
      else
      {
      
    }
    }
    public Startup(IHostingEnvironment env)
    {
      var builder = new ConfigurationBuilder()
          .SetBasePath(env.ContentRootPath)
          .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
          .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
          .AddEnvironmentVariables();
      Configuration = builder.Build();
    }

    public IConfigurationRoot Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      var connectionString2 = DatabaseSettings.getConnectionString();
                            services.AddDbContext<DatabaseContext>(opts => opts.UseNpgsql(connectionString2));
      




            /*
             * ADD Identity for login
             */
            services.AddIdentity<ApplicationUser, ApplicationRole>()
            .AddEntityFrameworkStores<DatabaseContext>()
            .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 0;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;

            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings

                options.Cookie.HttpOnly = false;
                options.Cookie.Expiration = TimeSpan.FromDays(150);
                options.LoginPath = "/Account/Login"; // If the LoginPath is not set here, ASP.NET Core will default to /Account/Login
                options.LogoutPath = "/Account/Logout"; // If the LogoutPath is not set here, ASP.NET Core will default to /Account/Logout
                options.AccessDeniedPath = "/Account/AccessDenied"; // If the AccessDeniedPath is not set here, ASP.NET Core will default to /Account/AccessDenied
                options.SlidingExpiration = true;
            });


            services.AddAuthentication()
                    .AddCookie(cfg => { cfg.SlidingExpiration = true; })
                     .AddJwtBearer(cfg =>
                      {
                          cfg.RequireHttpsMetadata = false;
                          cfg.SaveToken = true;

                          cfg.TokenValidationParameters = new TokenValidationParameters()
                          {
                              ValidIssuer = "Tokens:Issuer",
                              ValidAudience = "Tokens:Issuer",
                              IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("I-Am-A-Key5244512e79775268374231315e41507e3f6e72734c40397a7b2851674b"))
                          };

                      });


            // Add framework services.
      services.AddMvc();
      services.AddNodeServices();

      var connectionStringBuilder = new Microsoft.Data.Sqlite.SqliteConnectionStringBuilder { DataSource = "spa.db" };
      var connectionString = connectionStringBuilder.ToString();

      services.AddDbContext<SpaDbContext>(options =>
          options.UseSqlite(connectionString));

      services.Configure<AppConfiguration>(Configuration.GetSection("AppConfiguration"));

      // Register the Swagger generator, defining one or more Swagger documents
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new Info { Title = "Angular 4.0 Universal & ASP.NET Core advanced starter-kit web API", Version = "v1" });
      });


 

    
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, SpaDbContext context)
    {
      loggerFactory.AddConsole(Configuration.GetSection("Logging"));
      loggerFactory.AddDebug();

      app.UseStaticFiles();

     // DbInitializer.Initialize(context);
      app.UseAuthentication();
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
       /* app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
        {
          HotModuleReplacement = true,
          HotModuleReplacementEndpoint = "/dist/__webpack_hmr"
        });*/
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
          c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        });

        // Enable middleware to serve swagger-ui (HTML, JS, CSS etc.), specifying the Swagger JSON endpoint.


        app.MapWhen(x => !x.Request.Path.Value.StartsWith("/swagger", StringComparison.OrdinalIgnoreCase), builder =>
        {
          builder.UseMvc(routes =>
          {
            routes.MapSpaFallbackRoute(
                name: "spa-fallback",
                defaults: new { controller = "Home", action = "Index" });
          });
        });
      }
      else
      {
        app.UseMvc(routes =>
        {
          routes.MapRoute(
           name: "default",
           template: "{controller=Home}/{action=Index}/{id?}");

          routes.MapRoute(
           "Sitemap",
           "sitemap.xml",
           new { controller = "Home", action = "SitemapXml" });

    
          routes.MapSpaFallbackRoute(
            name: "spa-fallback",
            defaults: new { controller = "Home", action = "Index" });

        });
        app.UseExceptionHandler("/Home/Error");
      }
    }
  }
}
