using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace MasterclassApiIntegrationTests
{
    // Integration test tutorial: https://www.youtube.com/watch?v=RXSPCIrrjHc
    internal class MasterclassApiWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                // Remove the normal connection string/database options
                // Replace it with different settings for testing purposes
            });
        }
    }
}
