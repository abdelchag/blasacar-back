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
using Serilog.Formatting.Compact;

namespace blasa.travel.web
{
    public class Program
    {
        public static void Main(string[] args)
        {


            //Initialize Logger
            Log.Logger = new LoggerConfiguration()
              .Enrich.FromLogContext()
              .Enrich.WithMachineName()
              .Enrich.WithProcessId()
              .Enrich.WithThreadId()
              .WriteTo.Console(new RenderedCompactJsonFormatter())
               .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
               .MinimumLevel.Override("System", LogEventLevel.Warning)
              .WriteTo.Debug(outputTemplate: DateTime.Now.ToString())
              .WriteTo.File("D:\\Logs\\log-blasa.travel-.txt", rollingInterval: RollingInterval.Day)
              //.WriteTo.File(new RenderedCompactJsonFormatter(), "D:\\Logs\\log-blasa.travel-.json", rollingInterval: RollingInterval.Day)
              .CreateLogger();
            try
            {
                Log.Information("Application blasa.travel.web Starting.");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "The Application blasa.travel.web failed to start.");
            }
            finally
            {
                Log.CloseAndFlush();
            }


        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>           

        Host.CreateDefaultBuilder(args)
                .UseSerilog() //Uses Serilog instead of default .NET Logger
                .ConfigureWebHostDefaults(webBuilder =>
                {
            webBuilder.UseStartup<Startup>();
        });
    }
}
