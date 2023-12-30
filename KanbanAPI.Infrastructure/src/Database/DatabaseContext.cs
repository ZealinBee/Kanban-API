using Npgsql;
using Microsoft.EntityFrameworkCore;

using KanbanAPI.Domain;
using KanbanAPI.Business;

namespace KanbanAPI.Infrastructure;

public class DatabaseContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Board> Boards { get; set; }
    public DbSet<UserBoard> UserBoards { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserBoard>()
            .HasKey(ub => new { ub.UserId, ub.BoardId });

        modelBuilder.Entity<UserBoard>()
            .HasOne(ub => ub.User)
            .WithMany(u => u.UserBoards)
            .HasForeignKey(ub => ub.UserId);

        modelBuilder.Entity<UserBoard>()
            .HasOne(ub => ub.Board)
            .WithMany(b => b.UserBoards)
            .HasForeignKey(ub => ub.BoardId);


    }
}
