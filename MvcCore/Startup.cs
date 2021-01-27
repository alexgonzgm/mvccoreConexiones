using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MvcCore.Data;
using MvcCore.Helpers;
using MvcCore.repositories;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace MvcCore
{
    public class Startup
    {
        IConfiguration Configuration;
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            string cadenasql = this.Configuration.GetConnectionString("cadenasqlhospitalcasa");
            string cadenaoracle = this.Configuration.GetConnectionString("cadenaoraclehospital");
            string cadenaMysql = this.Configuration.GetConnectionString("cadenamysql");

            services.AddTransient<PathProvider>();
            services.AddTransient<UploadService>();
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddTransient<MailService>();

            //**********REPOSIORIES***************
            services.AddTransient<RepositoryJoyerias>();
            services.AddTransient<RepositoryAlumnos>();
            services.AddTransient<IRepositoryHospital, RepositoryHospital>();
            //*****SQL
            // services.AddTransient<IRepositoryDepartamentos ,RepositoryDepartamentosSQL>();
            //*****ORACLE
           services.AddTransient<IRepositoryDepartamentos>(x => new RepositoryDepartamentosOracle(cadenaoracle));
            //*****MySql
             //services.AddTransient<IRepositoryDepartamentos,RepositoryDepartamentosMySql>();

            //**********CONTEXT***************

         
            services.AddDbContext<HospitalContext>(options => options.UseSqlServer(cadenasql));
            //services.AddDbContext<HospitalContext>(options => options.UseMySql(cadenaMysql,ServerVersion.AutoDetect(cadenaMysql)));
            //services.AddDbContextPool<HospitalContext>(options => options.UseMySql(cadenaMysql, ServerVersion.AutoDetect(cadenaMysql)));

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app
            , IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}
