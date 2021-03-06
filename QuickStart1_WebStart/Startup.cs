﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using QuickStart1_WebStart.Data;
using QuickStart1_WebStart.Extensions;

namespace QuickStart1_WebStart
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddMvc();
            services.AddOptions();
            services.Configure<TempUser>(Configuration.GetSection("TempUser"));

            var serviceProvider = services.BuildServiceProvider();

            var optionsMonitor = serviceProvider.GetService<IOptionsMonitor<TempUser>>();

            optionsMonitor.OnChange(OnChanged);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            int value = 0;
            app.UseMyMiddleware(value);
            //app.UseMvc();
        }

        public void OnChanged(TempUser user)
        {
            Console.WriteLine("tempuser changed " + user.UserName);
        }
    }
}
