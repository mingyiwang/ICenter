﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebAPI
{
    public class Startup
    {

        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                
                .AddEnvironmentVariables()
                .Build();
        }       

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
             services.AddMvc();
        }

        
        public void Configure(IApplicationBuilder app)
        {
            
            Middlewares
                .Of(app)
                .With(context =>
                {
                    
                    
                })
                .With(context =>
                {
                    
                })
                .First(context =>
                {
                    
                })
                .Last(context =>
                {
                    
                })
                .Build();

        }

    }
}
