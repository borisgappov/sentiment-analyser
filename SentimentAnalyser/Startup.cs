using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SentimentAnalyser.Data;
using SentimentAnalyser.Infrastructure.Swagger;
using SentimentAnalyser.Models.Converters;
using SentimentAnalyser.Models.Entities;

namespace SentimentAnalyser
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddHostedService<MigratorHostedService>();

            services.AddIdentityServer().AddApiAuthorization<ApplicationUser, ApplicationDbContext>();
            services.AddAuthentication().AddIdentityServerJwt();
            services.AddControllersWithViews();
            services.AddRazorPages();
            
            services.AddApiVersioning();
            if (_env.IsDevelopment())
                services.AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo {Title = "Sentiment Analyser API", Version = "v1"});
                    options.SchemaFilter<EnumSchemaFilter>();
                });

            Mapper.Initialize();

            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/dist"; });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            if (!env.IsDevelopment()) app.UseSpaStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "default",
                    "{controller}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(
                    options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", "Sentiment Analyser API"); });
            }

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                /*
                //spa.UseAngularCliServer("start");  
                because of a .net core 3 + Angular 9 bug
                please start the client manually (to to ClientApp dir and run: npm start)
                https://github.com/dotnet/aspnetcore/issues/17277                
                */
                if (env.IsDevelopment()) spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
            });
        }
    }
}