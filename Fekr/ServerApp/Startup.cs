using System;
using Domain.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using ServerApp.Helpers;
using ServerApp.Helpers.Admin;
using ServerApp.Helpers.Enseignant;
using ServerApp.Helpers.Parent;
using ServerApp.Services;
using Service;
using Service.IConfiguration;
using Service.Repository;
using Service.Repository.Classes;
using Service.Repository.Decids;
using Service.Repository.EmploiDuTemp;
using Service.Repository.Enseignant;
using Service.Repository.Etudiant;
using Service.Repository.ModuleEtudiant;
using Service.Repository.Modules;
using Service.Repository.Moyenne;
using Service.Repository.Notes;
using Service.Repository.Plan_etude;
using Service.Repository.Societes;

namespace ServerApp
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // add services to the DI container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => 
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.WithOrigins("*"));
            }
            );
            services.AddControllers().AddNewtonsoftJson(option =>
                 {
                    option.SerializerSettings.ContractResolver =
                         new CamelCasePropertyNamesContractResolver();
                 });

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
            services.AddScoped<IAdminApiRepo, AdminApiRepo>();
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
            services.AddScoped<IPlanEtudeApiRepo, PlanEtudeApiRepo>();
            services.AddScoped<INotesApiRepo, NotesApiRepo>();
            services.AddScoped<IMoyenneApiRepo, MoyenneApiRepo>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IModuleEtudiant, ModuleEtudiantApiRepo>();
            services.AddScoped<IEmploiDuTempRepo, EmploiDuTempRepo>();
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
