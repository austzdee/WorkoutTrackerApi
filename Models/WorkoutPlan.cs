namespace WorkoutTrackerApi.Models;

// Represents a workout plan
public class WorkoutPlan
{
    // Primary Key
    public int Id { get; set; }

    // Workout title
    public string Title { get; set; } = string.Empty;

    // Optional workout notes/comments
    public string? Notes { get; set; }

    // Scheduled workout date and time
    public DateTime ScheduledDate { get; set; }

    // Foreign Key:
    // Links workout plan to a user
    public int UserId { get; set; }

    // Navigation Property:
    // Workout belongs to one user
    public User? User { get; set; }

    // Navigation Property:
    // Workout can contain multiple exercises
    public List<Exercise> Exercises { get; set; } = new();
}