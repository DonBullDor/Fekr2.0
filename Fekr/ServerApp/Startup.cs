using System;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

using Data;

using Newtonsoft.Json.Serialization;

using Service.Repository;
using Service.Repository.Classes;
using Service.Repository.Decids;
using Service.Repository.Enseignant;
using Service.Repository.Etudiant;
using Service.Repository.Modules;
using Service.Repository.Societes;

namespace ServerApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllersWithViews()
                .AddJsonOptions(opts =>
                {
                    opts.JsonSerializerOptions.IgnoreNullValues = true;
                });
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services
                .AddDbContext<Oracle1Context>(options =>
                {
                    options
                        .UseOracle(Configuration
                            .GetConnectionString("OracleConnection"));
                });
            services.AddScoped<IEtudiantApiRepo, EtudiantApiRepo>();
            services.AddScoped<IClassesApiRepo, ClasseApiRepo>();
            services.AddScoped<IModuleApiRepo, ModuleApiRepo>();
            services.AddScoped<IDecidsApiRepo, DecidsApiRepo>();
            services.AddScoped<ISocietesApiRepo, SocieteApiRepo>();
            services.AddScoped<IEnseignantApiRepo, EnseignantApiRepo>();

            services
                .AddControllers()
                .AddNewtonsoftJson(option =>
                {
                    option.SerializerSettings.ContractResolver =
                        new CamelCasePropertyNamesContractResolver();
                });

            services
                .AddSwaggerGen(options =>
                {
                    options
                        .SwaggerDoc("v1",
                        new OpenApiInfo {
                            Title = "Fekr API",
                            Version = "v1"
                        });
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app
                .UseEndpoints(endpoints =>
                {
                    endpoints
                        .MapControllerRoute(name: "default",
                        pattern: "{controller=Home}/{action=Index}/{id?}");
                    endpoints
                        .MapControllerRoute(name: "angular_fallback",
                        pattern: "{target:regex(admin|classe|enseignant|etudiant|module|Parent)}/{*catchall}",
                        defaults: new {
                            controller = "Home",
                            action = "Index"
                        });
                });

            app.UseSwagger();
            app
                .UseSwaggerUI(options =>
                {
                    options
                        .SwaggerEndpoint("/swagger/v1/swagger.json",
                        "Fekr API");
                });

            app
                .UseSpa(spa =>
                {
                    spa.Options.SourcePath = "../ClientApp";
                    spa.UseAngularCliServer("start");
                });
        }
    }
}
