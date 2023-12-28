using Npgsql;
using Microsoft.EntityFrameworkCore;

using KanbanAPI.Domain;
using KanbanAPI.Business;

namespace KanbanAPI.Infrastructure;

public class DatabaseContext : DbContext
{
    private readonly IConfiguration _configuration;
    public DbSet<User> Users { get; set; }

    public DatabaseContext(IConfiguration configuration, DbContextOptions options) : base(options)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var builder = new NpgsqlConnectionStringBuilder(_configuration.GetConnectionString("DefaultConnection"));
        optionsBuilder.UseNpgsql(builder.ConnectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}
