using System.IO;
using Microsoft.AspNetCore.Hosting;

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
