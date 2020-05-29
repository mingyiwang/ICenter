using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace WebAPI
{
    public class Program
    {

        public static void Main(string[] args)
        {
            WebHost.CreateDefaultBuilder<Startup>(args)
                   .UseKestrel()
                   .UseIISIntegration()
                   .UseContentRoot(Directory.GetCurrentDirectory())
                   .Build()
                   .Run();
        }

    }
}
