using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace SupportApp.Infrastructure.Persistence;

public class SupportAppDbContextFactory: IDesignTimeDbContextFactory<SupportAppDbContext>
    {
    public SupportAppDbContext CreateDbContext (string[] args)
        {
        var basePath = Directory.GetCurrentDirectory();

        var config = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        var connectionString = config
            .GetSection("DatabaseOptions")["ConnectionString"];

        var optionsBuilder = new DbContextOptionsBuilder<SupportAppDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new SupportAppDbContext(optionsBuilder.Options);
        }
    }