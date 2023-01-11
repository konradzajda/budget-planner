using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Tivix.BudgetPlanner.Application;
using Tivix.BudgetPlanner.Infrastructure;

namespace Tivix.BudgetPlanner.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            builder.Services.AddApi();
            builder.Services.AddApplication();
            builder.Services.AddInfrastructure(configuration);

            var app = builder.Build();

            await app.RunDatabaseMigrationsAsync();
            
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseAuthorization();

            app.MapControllers();

            await app.RunAsync();
        }
    }
}

