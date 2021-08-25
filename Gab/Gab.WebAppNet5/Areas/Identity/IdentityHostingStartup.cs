using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Gab.WebAppNet5.Areas.Identity.IdentityHostingStartup))]
namespace Gab.WebAppNet5.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}