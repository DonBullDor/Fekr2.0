using System;
using Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using ServerApp.Helpers;
using ServerApp.Helpers.Admin;
using ServerApp.Helpers.Enseignant;
using ServerApp.Helpers.Parent;
using ServerApp.Services;
using Service.Repository;
using Service.Repository.Classes;
using Service.Repository.Decids;
using Service.Repository.Enseignant;
using Service.Repository.Etudiant;
using Service.Repository.Modules;
using Service.Repository.Societes;

namespace ServerApp
{
    public class StartupBackup
    {
        public IConfiguration Configuration { get; }

        public StartupBackup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // add services to the DI container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers();

            services
                .AddDbContext<Oracle1Context>(options =>
                {
                    options
                        .UseOracle(Configuration
                            .GetConnectionString("OracleConnection"));
                });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IEtudiantApiRepo, EtudiantApiRepo>();
            services.AddScoped<IClassesApiRepo, ClasseApiRepo>();
            services.AddScoped<IModuleApiRepo, ModuleApiRepo>();
            services.AddScoped<IDecidsApiRepo, DecidsApiRepo>();
            services.AddScoped<ISocietesApiRepo, SocieteApiRepo>();
            services.AddScoped<IEnseignantApiRepo, EnseignantApiRepo>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAdminLoginService, AdminLoginService>();
            services
                .AddScoped<IEnseignantLoginService, EnseignantLoginService>();
            services.AddScoped<IParentLoginService, ParentLoginService>();
            // configure strongly typed settings object
            services
                .Configure<AppSettings>(Configuration
                    .GetSection("AppSettings"));

            // configure DI for application services
            services.AddScoped<IUserService, UserService>();
        }

        // configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            // global cors policy
            app
                .UseCors(x =>
                    x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            // custom jwt auth middleware
            app.UseMiddleware<JwtMiddleware>();
            app.UseMiddleware<AdminJwtMiddleware>();
            app.UseMiddleware<EnseignantJwtMiddleware>();
            app.UseMiddleware<ParentJwtMiddleware>();
            app.UseEndpoints(x => x.MapControllers());
        }
    }
}
