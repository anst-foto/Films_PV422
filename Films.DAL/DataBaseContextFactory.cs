using System.IO;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Films.DAL;

public class DataBaseContextFactory : IDesignTimeDbContextFactory<DataBaseContext>
{
    public DataBaseContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = config.GetConnectionString("DefaultConnection");
        return new DataBaseContext(connectionString);
    }
}
