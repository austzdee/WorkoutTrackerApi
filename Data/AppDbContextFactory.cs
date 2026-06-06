using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace WorkoutTrackerApi.Data;

// Provides AppDbContext configuration for EF Core design-time commands
public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        // Configure SQLite connection for migrations
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        optionsBuilder.UseNpgsql(
    "Host=localhost;Port=5432;Database=WorkoutTrackerDb;Username=postgres;Password=postgres"
);

        return new AppDbContext(optionsBuilder.Options);
    }
}