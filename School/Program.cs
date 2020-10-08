using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace School {
    public class Program {
        public static void Main(string[] args) {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => {
                    webBuilder.ConfigureAppConfiguration(Configuration)
                    .UseStartup<Startup>()
                    .UseSerilog();
                });
        private static void Configuration(WebHostBuilderContext arg1, IConfigurationBuilder arg2) {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.ControlledBy(new Serilog.Core.LoggingLevelSwitch {
                    MinimumLevel = LogEventLevel.Information
                })
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", Serilog.Events.LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.RollingFile($"/Logs/rolling")
                .CreateLogger();
        }
    }
}
