using System;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Tivix.BudgetPlanner.Infrastructure;

namespace Tivix.BudgetPlanner.Application.Internal;

public abstract class SqliteBasedTests : IDisposable
{
    private readonly SqliteConnection _connection;

    protected readonly BudgetsContext Context;
    
    protected SqliteBasedTests()
    {
        _connection = new SqliteConnection("Data Source=./tests.db");
        
        var contextOptions = new DbContextOptionsBuilder<BudgetsContext>()
            .UseSqlite(_connection, lite => lite.MigrationsAssembly("tivix_api"))
            .Options;
        
        
        Context = new BudgetsContext(contextOptions);
        Context.Database.Migrate();

    }

    public void Dispose()
    {
        _connection.Dispose();
    }
}