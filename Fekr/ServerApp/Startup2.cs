using System;
using Data;
using Domain.Models;
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
    public class Startup2
    {
        public IConfiguration Configuration { get; }

        public Startup2(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // old
        /*
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services
                .AddControllersWithViews()
                .AddJsonOptions(opts =>
                {
                    opts.JsonSerializerOptions.IgnoreNullValues = true;
                });
            services
                .Configure<AppSettings>(Configuration
                    .GetSection("AppSettings"));
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
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAdminLoginService, AdminLoginService>();
            services
                .AddScoped<IEnseignantLoginService, EnseignantLoginService>();
            services.AddScoped<IParentLoginService, ParentLoginService>();
            // services
            //     .AddControllers()
            //     .AddNewtonsoftJson(option =>
            //     {
            //         option.SerializerSettings.ContractResolver =
            //             new CamelCasePropertyNamesContractResolver();
            //     });

            services
                .AddSwaggerGen(options =>
                {
                    options
                        .SwaggerDoc("v1",
                        new OpenApiInfo { Title = "Fekr API", Version = "v1" });
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

            app
                .UseCors(x =>
                    x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseAuthorization();
            app.UseMiddleware<JwtMiddleware>();
            app.UseMiddleware<AdminJwtMiddleware>();
            app.UseMiddleware<EnseignantJwtMiddleware>();
            app.UseMiddleware<ParentJwtMiddleware>();

            // global cors policy
            // app.UseCors("CorsPolicy");
            // global cors policy
            // app
            //     .UseCors(x =>
            //         x
            //             .AllowAnyOrigin()
            //             .AllowAnyMethod()
            //             .SetIsOriginAllowed(origin => true) // allow any origin
            //             .AllowAnyHeader());
            // x =>
            //     x
            //         .AllowAnyMethod()
            //         .AllowAnyHeader()
            //         .SetIsOriginAllowed(origin => true) // allow any origin
            //         .AllowCredentials()); // allow credentials
            // app
            //     .UseEndpoints(endpoints =>
            //     {
            //         endpoints
            //             .MapControllerRoute(name: "default",
            //             pattern: "{controller=Home}/{action=Index}/{id?}");
            //         endpoints
            //             .MapControllerRoute(name: "angular_fallback",
            //             pattern: "{target:regex(admin|classe|enseignant|etudiant|module|Parent)}/{*catchall}",
            //             defaults: new {
            //                 controller = "Home",
            //                 action = "Index"
            //             });
            //     });
            app.UseEndpoints(x => x.MapControllers());
            app.UseSwagger();
            app
                .UseSwaggerUI(options =>
                {
                    options
                        .SwaggerEndpoint("/swagger/v1/swagger.json",
                        "Fekr API");
                });

            // app
            //     .UseSpa(spa =>
            //     {
            //         spa.Options.SourcePath = "../ClientApp";
            //         spa.UseAngularCliServer("start");
            //         // spa
            //         //     .ApplicationBuilder
            //         //     .UseCors(builder =>
            //         //     {
            //         //         // Must specify Methods
            //         //         builder.WithMethods("GET");

            //         //         // Case insensitive headers
            //         //         builder.WithHeaders("Authorization");

            //         //         // Can supply a list or one by one, either is fine
            //         //         builder.WithOrigins("http://localhost:5000");
            //         //         builder.WithOrigins("https://localhost:5001");
            //         //     });
            //     });
        }
        */
        //new
        
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

            app.UseEndpoints(x => x.MapControllers());
        }
        
    }
}
