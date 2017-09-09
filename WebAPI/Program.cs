using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;

namespace WebAPI
{

    public class Program
    {

        public static void Main(string[] args)
        {
            CreateWebHost().Run();
        }

        private static IWebHost CreateWebHost()
        {
            // Read Configuration Files
            
            return new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .Build();

        }


    }
}
