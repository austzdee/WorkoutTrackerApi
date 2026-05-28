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

        optionsBuilder.UseSqlite("Data Source=WorkoutTracker.db");

        return new AppDbContext(optionsBuilder.Options);
    }
}