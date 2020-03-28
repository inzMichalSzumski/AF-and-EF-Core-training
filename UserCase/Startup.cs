using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using UserCase.Models;
using UserCase.Services;

[assembly: FunctionsStartup(typeof(UserCase.Startup))]

namespace UserCase
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: false)
                .AddEnvironmentVariables()
                .Build();

            builder.Services.AddDbContext<userscaseContext>(optionsBuilder => {
                    optionsBuilder.UseSqlServer(config.GetConnectionString("SQLConnectionString"));
            });

            builder.Services.AddScoped<IDbService, DbService>();
        }

    }
}