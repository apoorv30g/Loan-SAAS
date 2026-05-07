using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using LoanSaas.Backend.Models;

namespace LoanSaas.Backend.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Client> Clients { get; set; }
    public DbSet<Lead> Leads { get; set; }
    public DbSet<CallLog> CallLogs { get; set; }
    public DbSet<ClientCost> ClientCosts { get; set; }
}

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        // Resolve project root reliably whether called from CLI or EF tools
        var assemblyDir = Path.GetDirectoryName(typeof(AppDbContextFactory).Assembly.Location)!;
        var basePath = File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"))
            ? Directory.GetCurrentDirectory()
            : Path.GetFullPath(Path.Combine(assemblyDir, "..", "..", ".."));

        var config = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json")
            .AddJsonFile("appsettings.Development.json", optional: true)
            .Build();

        var connectionString = config.GetConnectionString("Default")
            ?? throw new InvalidOperationException($"Connection string 'Default' not found in {basePath}.");

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new AppDbContext(optionsBuilder.Options);
    }
}
