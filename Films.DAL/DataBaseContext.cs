using Films.Model;
using Microsoft.EntityFrameworkCore;

namespace Films.DAL;

public class DataBaseContext : DbContext
{
    private readonly string _connectionString;

    public DbSet<Film> Films { get; set; }

    public DataBaseContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_connectionString);
    }
}
