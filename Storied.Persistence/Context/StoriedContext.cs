using Microsoft.EntityFrameworkCore;
using Storied.Domain.Common.Enums;
using Storied.Domain.Entities;

namespace Storied.Persistence.Context;

/// <summary>  
/// Represents the database context for the Storied application.  
/// </summary>  
/// <remarks>  
/// This context is used to interact with the database and provides DbSet properties for entities.  
/// </remarks>  
public class StoriedContext(DbContextOptions<StoriedContext> options) : DbContext(options)
{
    /// <summary>  
    /// Gets or sets the DbSet for the Person entity.  
    /// </summary>  
    public DbSet<Person> Persons { get; set; }

    /// <summary>  
    /// Configures the model using the Fluent API.  
    /// </summary>  
    /// <param name="modelBuilder">The builder used to configure the model.</param>  
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(p => p.Id);

            entity.Property(p => p.Id).ValueGeneratedOnAdd();

            entity.Property(p => p.GivenName).IsRequired().HasMaxLength(50);

            entity.Property(p => p.SurName).HasMaxLength(50);

            entity.Property(p => p.Gender).HasConversion(
                g => g.ToString(),
                g => (Gender)Enum.Parse(typeof(Gender), g)
            );
            entity.Property(p => p.BirthDate).HasColumnType("date");

            entity.Property(p => p.BirthLocation).HasMaxLength(100);

            entity.Property(p => p.DeathDate).HasColumnType("date");

            entity.Property(p => p.DeathLocation).HasMaxLength(100);

        });
    }
}
