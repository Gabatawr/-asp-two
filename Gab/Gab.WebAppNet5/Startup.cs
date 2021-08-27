using System.Text.Json.Serialization;
using Gab.WebAppNet5.Data;
using Gab.WebAppNet5.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Gab.WebAppNet5
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration) =>
        Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
          services.AddDbContext<ApplicationDbContext>(options =>
              options.UseSqlServer(
                  Configuration.GetConnectionString("DefaultConnection")));

          services.AddDatabaseDeveloperPageExceptionFilter();

          services.AddDefaultIdentity<User>(options =>
                  options.SignIn.RequireConfirmedAccount = true)
              .AddEntityFrameworkStores<ApplicationDbContext>();

          services.AddControllersWithViews().AddJsonOptions(x =>
              x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
          //.AddJsonOptions(x =>
          // x.JsonSerializerOptions.IgnoreNullValues = true);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
          if (env.IsDevelopment())
          {
              app.UseDeveloperExceptionPage();
              app.UseMigrationsEndPoint();
          }
          else
          {
              app.UseExceptionHandler("/Home/Error");
              app.UseHsts();
          }

          app.UseHttpsRedirection();

          app.UseStaticFiles();

          app.UseRouting();

          app.UseAuthentication();
          app.UseAuthorization();

          app.UseEndpoints(endpoints =>
          {
              endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "{controller=Ajax}/{action=Index}/{id?}");
              endpoints.MapRazorPages();
          });
        }
    }
}
