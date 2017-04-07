using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace ExampleApp {
    public class Program {
        public static void Main(string[] args) {
            var config = new ConfigurationBuilder()
                 .AddCommandLine(args)
                 .AddEnvironmentVariables()
                 .Build();

            if ((config["INITDB"] ?? "false") == "true") {
                System.Console.WriteLine("Preparing Database...");
                Models.SeedData.EnsurePopulated(new Models.ProductDbContext());
                System.Console.WriteLine("Database Preparation Complete");
            } else {
                System.Console.WriteLine("Starting ASP.NET...");
                var host = new WebHostBuilder()
                .UseConfiguration(config)
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

                host.Run();
            }
        }
    }
}
