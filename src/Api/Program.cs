using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Tivix.BudgetPlanner.Api.Extensions;
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
                .AddJsonFile("firebase.json")
                .Build();

            builder.Services.AddApi(configuration);
            builder.Services.AddApplication();
            builder.Services.AddInfrastructure(configuration);

            var app = builder.Build();

            await app.RunDatabaseMigrationsAsync();
            
            app.UseSwagger()
                .UseSwaggerUI(options =>
                {
                    options.OAuthClientId(configuration.GetOAuthClientId());
                    options.OAuthClientSecret(configuration.GetOAuthClientSecret());
                    options.OAuthUsePkce();
                });
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            await app.RunAsync();
        }
    }
}

