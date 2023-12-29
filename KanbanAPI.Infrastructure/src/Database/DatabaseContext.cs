using Npgsql;
using Microsoft.EntityFrameworkCore;

using KanbanAPI.Domain;
using KanbanAPI.Business;

namespace KanbanAPI.Infrastructure;

public class DatabaseContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}
