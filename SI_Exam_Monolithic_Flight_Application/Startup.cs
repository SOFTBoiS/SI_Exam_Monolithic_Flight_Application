using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CamundaClient;
using Grpc.Core;
using Microsoft.AspNetCore.Http;
using SI_Exam_Monolithic_Flight_Application;
using SI_Exam_Monolithic_Flight_Application.gRPC;
using SI_Exam_Monolithic_Flight_Application.Models.DTOs;
using SI_Exam_Monolithic_Flight_Application.Utils;

namespace SI_Exam_Monolithic_Flight_Application
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddGrpc();
            services.AddSession(options =>
            {
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<Service>();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                
            });
            
            //var xml = @"
            //        <carId>etmongoidher</carId>
            //        <username>adam</username>
            //        <startDate>2020-12-17T00:00:00+01:00</startDate>
            //    <endDate>2020-12-17T00:00:00+01:00</endDate>
            //    <price>10000</price>
            //    ";
            //var car = XmlUtils<CarBookingModel>.DeserializeToType(xml);
            //Debug.WriteLine(car.username);
            //Debug.WriteLine(car.carId);
            CamundaEngineClient camunda = new CamundaEngineClient();            
            camunda.Startup(); 
        }
    }
}

