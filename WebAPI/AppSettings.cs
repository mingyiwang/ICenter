using Microsoft.Extensions.Configuration;

namespace WebAPI
{

    public sealed class AppSettings
    {
        public IConfigurationRoot Configuration { get; }

        private AppSettings( IConfigurationRoot configuration)
        {
            Configuration = configuration;
        }

        public static AppSettings Init()
        {
           return new AppSettings(new ConfigurationBuilder()
                .AddJsonFile("")
                .Build());
        }


    }

}