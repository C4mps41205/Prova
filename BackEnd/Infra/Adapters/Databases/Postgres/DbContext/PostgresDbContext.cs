using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace Infra.Adapters.Databases.Postgres.DbContext;

/// <summary>
/// Represents the PostgreSQL database context for the application.
/// </summary>
/// <remarks>
/// The PostgresDbContext class inherits from the Microsoft.EntityFrameworkCore.DbContext class and provides access to the User and TaskToDo entities in the database.
/// It configures the database connection and the relationships between the entities.
/// </remarks>
public class PostgresDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    /// <summary>
    /// Initializes a new instance of the PostgresDbContext class with the specified options.
    /// </summary>
    /// <param name="options">The database context options.</param>
    public PostgresDbContext(DbContextOptions options) : base(options) {}

    /// <summary>
    /// Initializes a new instance of the PostgresDbContext class.
    /// </summary>
    public PostgresDbContext()
    {
    }

    /// <summary>
    /// Gets or sets the DbSet for the User entity.
    /// </summary>
    public DbSet<Domain.Entities.User> User { get; set; }

    /// <summary>
    /// Gets or sets the DbSet for the TaskToDo entity.
    /// </summary>
    public DbSet<TaskToDo> TaskToDo { get; set; }
    
    public DbSet<Domain.Entities.Category> Category { get; set; }

    /// <summary>
    /// Configures the model for the database context.
    /// </summary>
    /// <param name="modelBuilder">The model builder used to configure the model.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure the key for the TaskToDo entity
        modelBuilder.Entity<TaskToDo>()
            .HasKey(t => t.Id);

        // Configure the key for the User entity
        modelBuilder.Entity<Domain.Entities.User>()
            .HasKey(u => u.Id);
    
        // Configure the key for the Category entity
        modelBuilder.Entity<Domain.Entities.Category>()
            .HasKey(c => c.Id);

        // Configure the relationship between TaskToDo and User entities
        modelBuilder.Entity<TaskToDo>()
            .HasOne(t => t.User)
            .WithMany(u => u.Tasks)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure the relationship between TaskToDo and Category entities
        modelBuilder.Entity<TaskToDo>()
            .HasOne(t => t.Category)
            .WithMany(c => c.Tasks)
            .HasForeignKey(t => t.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);
        
        
        // Configure the relationship between TaskToDo and User entities
        modelBuilder.Entity<Domain.Entities.Category>()
            .HasOne(c => c.User)
            .WithMany(u => u.Categories)
            .HasForeignKey(c => c.UserId);


        base.OnModelCreating(modelBuilder);
    }


    /// <summary>
    /// Configures the database connection options for the database context.
    /// </summary>
    /// <param name="optionsBuilder">The options builder used to configure the database connection.</param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseNpgsql(connectionString);
        }
    }
}