using AIMLChatBot.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AIMLChatBot.Helper.CustomHelper
{
    public class ConfigHelper 
    {

        #region Methods

        public static string GetSettingValue(string MainKey, string SubKey)
        {
            return Configuration.GetSection(MainKey).GetValue<string>(SubKey);
        }

        #endregion


        #region Properties

        public static IConfigurationRoot _configuration;
        public static IConfigurationRoot Configuration
        {
            get
            {
                if (_configuration == null)
                {

                    string environmentVariable = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

                    IConfigurationBuilder builder = new ConfigurationBuilder();
                    builder.SetBasePath(Directory.GetCurrentDirectory());
                    builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                    if (!string.IsNullOrEmpty(environmentVariable))
                    {
                        builder.AddJsonFile($"appsettings.{environmentVariable}.json", optional: false, reloadOnChange: true);
                    }
                    builder.AddEnvironmentVariables();
                   
                    
                    _configuration = builder.Build();
                }
                return _configuration;
            }
        }


        public static AppSetings Settings
        {
            get
            {
                return Configuration.GetSection("AppSettings").Get<AppSetings>();
            }
        }
        #endregion
    }
}
