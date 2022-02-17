using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using XperienceAdapter.Logging;

namespace MedioClinic
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                // a call to the AddXperience method you've just implemented.
                .ConfigureLogging(logging => logging.AddXperience())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }

    //In this example, the logging infrastructure is invoked in the Main method, according to the best practice outlined by Microsoft. 
    //Experience logger relies on a service, whose object is constructed by the Experience built-in DI container.
    //During start, that container becomes functional only after the core Experience objects have been built out of database data.
    //This means that if your database server happens to have slower initial connection times, the logger may start failing at times, resulting in exception messages at startup.
    //In such case, we recommend invoking your logger after AddKentico() is run in Startup.ConfigureServices.
}
