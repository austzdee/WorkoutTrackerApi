using Microsoft.EntityFrameworkCore;
using WorkoutTrackerApi.Models;

namespace WorkoutTrackerApi.Data;

// Main database context
public class AppDbContext : DbContext
{
    // Constructor
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    // Users table
    public DbSet<User> Users => Set<User>();

    // WorkoutPlans table
    public DbSet<WorkoutPlan> WorkoutPlans => Set<WorkoutPlan>();

    // Exercises table
    public DbSet<Exercise> Exercises => Set<Exercise>();
}