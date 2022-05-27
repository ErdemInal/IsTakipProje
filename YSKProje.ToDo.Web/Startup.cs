using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YSKProje.ToDo.Web.Middlewares;
using Microsoft.AspNetCore.Routing.Constraints;
using YSKProje.ToDo.Web.Constraints;

namespace YSKProje.ToDo.Web
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
            services.AddSession();
            services.AddControllersWithViews();
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
            //app.UseStatusCodePages(); // olmayan sayfalar için hata kodu döndürme
            app.UseStatusCodePagesWithReExecute("/Home/PageError", "?code={0}");
            app.UseStaticFiles();
            //app.UseCustomStaticFiles();
            app.UseRouting();

            app.UseAuthorization();
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name:"areas",
                    pattern:"{area}/{controller=Home}/{action=Index}/{id?}"
                    );

                endpoints.MapControllerRoute(
                        name: "programlamaRoute",
                        pattern: "programlama/{dil}",
                        defaults: new { controller = "Home", action = "Index" },
                        //pattern: "{controller=Home}/{action=Index}/{id:int}",//sadece int deðer alýr kýsýtlama
                        constraints: new { dil = new Programlama() } //sadece int deðer alýr kýsýtlama
                        );

                endpoints.MapControllerRoute(
                        name: "kisi",
                        pattern:"kisiler",
                        defaults: new {controller= "Home" , action= "Index"}
                        //pattern: "{controller=Home}/{action=Index}/{id:int}",//sadece int deðer alýr kýsýtlama
                        //constraints: new { id = new IntRouteConstraint() }); //sadece int deðer alýr kýsýtlama
                        );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                    //pattern: "{controller=Home}/{action=Index}/{id:int}",//sadece int deðer alýr kýsýtlama
                    //constraints: new { id = new IntRouteConstraint() }); //sadece int deðer alýr kýsýtlama
                    );
        
        });
        }
    }
}
