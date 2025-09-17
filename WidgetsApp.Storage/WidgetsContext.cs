using Microsoft.EntityFrameworkCore;
using WidgetsApp.Storage.Models;

namespace WidgetsApp.Storage;

public class WidgetsContext : DbContext
{
    public WidgetsContext(DbContextOptions<WidgetsContext> options)
        : base(options)
    {
    }

    public WidgetsContext()
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=TodoLisDB;Integrated Security=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Fluent API
        modelBuilder.Entity<ListItem>()
            .HasKey(x => x.Id);
        
        modelBuilder.Entity<ListItem>()
            .Property(x => x.Content)
            .HasMaxLength(120)
            .IsRequired();

        modelBuilder.Entity<TodoList>()
            .HasKey(x => x.Id);

        modelBuilder.Entity<TodoList>()
            .Property(x => x.Title)
            .HasMaxLength(60)
            .IsRequired();
    }
}