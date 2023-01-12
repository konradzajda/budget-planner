using System;
using System.Linq;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using Tivix.BudgetPlanner.Application.Abstractions;
using Tivix.BudgetPlanner.Infrastructure;

namespace Tivix.BudgetPlanner.Application.Internal;

public abstract class SqliteBasedTests : IDisposable
{
    private readonly SqliteConnection _connection;

    protected readonly BudgetsContext Context;

    protected readonly IUserContextAccessor UserContext;
    
    protected SqliteBasedTests()
    {
        _connection = new SqliteConnection($"Data Source=./{Guid.NewGuid()}.db");
        
        var contextOptions = new DbContextOptionsBuilder<BudgetsContext>()
            .UseSqlite(_connection, lite => lite.MigrationsAssembly("tivix_api"))
            .Options;

        UserContext = Substitute.For<IUserContextAccessor>();
        UserContext.Email.Returns("email@email.com");
        
        Context = new BudgetsContext(contextOptions, Substitute.For<IUserContextAccessor>());
        var pendingMigrations = Context.Database.GetPendingMigrations();

        if (pendingMigrations.Any())
            Context.Database.EnsureCreated();
        

    }

    public void Dispose()
    {
        Context.Database.EnsureDeleted();
        _connection.Dispose();
    }
}