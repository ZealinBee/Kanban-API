using Npgsql;
using Microsoft.EntityFrameworkCore;

using KanbanAPI.Domain;
using KanbanAPI.Business;

namespace KanbanAPI.Infrastructure;

public class DatabaseContext : DbContext
{
    public DbSet<User> User { get; set; }
    public DbSet<Board> Board { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(e => e.Boards)
            .WithMany(e => e.Users);
    }
}
