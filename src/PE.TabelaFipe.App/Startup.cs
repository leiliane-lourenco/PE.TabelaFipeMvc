using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PE.TabelaFipe.Application.Services;
using PE.TabelaFipe.Repository.Models;
using PE.TabelaFipe.Repository.Repositories;

namespace PE.TabelaFipe.App
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
            var endpoints = Configuration.GetSection("Endpoints").Get<Endpoints>();
            services.AddSingleton(endpoints);

            services.AddAutoMapper(typeof(Startup));

            services.AddHttpClient<ITabelaFipeRepository, TabelaFipeRepository>();

            services.AddScoped<ITabelaFipeService, TabelaFipeService>();

            services.AddControllersWithViews();

            services.AddMemoryCache();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/erro/500");
                app.UseStatusCodePagesWithRedirects("/erro/{0}");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
