
using AIMLChatBot.Interface;
using AIMLChatBot.Models;
using AIMLChatBot.Models.Common;
using AIMLChatBot.Service;
using DataAccess.DBConnection;
using DataAccess.Interface;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace AIMLChatBot
{
    public class Startup
    {

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        //This package contains the Interfaces and functionality for retrieving config values/sections
        //Configuration could be loaded from from anywhere including json files, environment variable or azure keyword 
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddMvc(config =>
            {
                config.EnableEndpointRouting = false;
            });

            //service lifetime 
            services.AddSingleton<IUnitofWork, RepoSQLDBUnitofWork>();
            services.AddSingleton<IConnection, RepoSQLDBConnection>();
            services.AddSingleton<IChatBotInitialization, ChatBotInitialization>();

            services.AddApiVersioning(config =>
            {
                config.UseApiBehavior = false;
                // Specify the default API Version as 1.0
                config.DefaultApiVersion = new ApiVersion(1,0);
                // If the client hasn't specified the API version in the request, use the default API version number 
                config.AssumeDefaultVersionWhenUnspecified = true;
                // Advertise the API versions supported for the particular endpoint
                config.ReportApiVersions = true;
            });

            services.AddCors();
            
            services.Configure<AppSetings>(Configuration.GetSection(nameof(AppSetings)));


        }




        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //setting app domain
            AppDomain.CurrentDomain.SetData("ContentRootPath", env.ContentRootPath);
            AppDomain.CurrentDomain.SetData("WebRootPath", env.WebRootPath);

            //environment
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            //add middleware for use html, css, js and images 
            app.UseStaticFiles();

            app.UseRouting();

            app.UseMvcWithDefaultRoute();

            app.UseAuthorization();

            //middleware that help us to map controller in the controller class
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
