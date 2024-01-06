using Npgsql;
using Microsoft.EntityFrameworkCore;

using KanbanAPI.Domain;
using KanbanAPI.Business;

namespace KanbanAPI.Infrastructure;

public class DatabaseContext : DbContext
{
    public DbSet<User> User { get; set; }
    public DbSet<Board> Board { get; set; }
    public DbSet<Item> Item{ get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum<TaskStatus>();
        modelBuilder.Entity<User>()
            .HasMany(e => e.Boards)
            .WithMany(e => e.Users);
        modelBuilder.Entity<Board>()
            .HasMany(e => e.Items)
            .WithOne(e => e.Board);
        modelBuilder.Entity<Item>()
            .HasMany(e => e.Users)
            .WithMany(e => e.Items);
    }
}
