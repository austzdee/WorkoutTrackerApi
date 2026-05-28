namespace WorkoutTrackerApi.Models;

// Represents an application user
public class User
{
    // Primary Key
    public int Id { get; set; }

    // User full name
    public string FullName { get; set; } = string.Empty;

    // Unique email address
    public string Email { get; set; } = string.Empty;

    // Secure hashed password
    public string PasswordHash { get; set; } = string.Empty;

    // Navigation Property:
    // One user can have multiple workout plans
    public List<WorkoutPlan> WorkoutPlans { get; set; } = new();
}