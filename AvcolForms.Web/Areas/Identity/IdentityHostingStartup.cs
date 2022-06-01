[assembly: HostingStartup(typeof(AvcolForms.Web.Areas.Identity.IdentityHostingStartup))]
namespace AvcolForms.Web.Areas.Identity;

public class IdentityHostingStartup : IHostingStartup
{
    public void Configure(IWebHostBuilder builder)
    {
        builder.ConfigureServices((context, services) => {
        });
    }
}
