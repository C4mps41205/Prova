using Application.Repositories;
using Infra.Adapters.Databases.Postgres.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infra.Adapters.Databases.Postgres.Context;
/// <summary>
/// Provides a factory for creating instances of the <see cref="PostgresDbContext"/> class, which is an implementation of the <see cref="DbContext"/> class from Entity Framework Core.
/// The factory is responsible for configuring the database connection options using the connection string from the application's configuration.
/// </summary>
/// <remarks>
/// This class is part of the infrastructure layer of the application, and is responsible for providing a way to create instances of the database context class that can be used by the application's repositories.
/// </remarks>
public class PostgresContextFactory : IContextFactory
{
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Initializes a new instance of the <see cref="PostgresContextFactory"/> class.
    /// </summary>
    /// <param name="configuration">The application's configuration, which is used to retrieve the database connection string.</param>
    public PostgresContextFactory(IConfiguration configuration)
    {
        this._configuration = configuration;
    }

    /// <summary>
    /// Creates a new instance of the <see cref="PostgresDbContext"/> class, configured with the database connection options.
    /// </summary>
    /// <returns>A new instance of the <see cref="PostgresDbContext"/> class.</returns>
    public Microsoft.EntityFrameworkCore.DbContext CreateDbFactory()
    {
        var optionsBuilder = new DbContextOptionsBuilder<PostgresDbContext>();
        var connectionString = _configuration.GetConnectionString("DefaultConnection");

        optionsBuilder.UseNpgsql(connectionString);

        return new PostgresDbContext(optionsBuilder.Options);
    }
}