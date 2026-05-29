namespace WorkoutTrackerApi.Dtos;

// Data required to create a workout plan
public class CreateWorkoutPlanDto
{
    // Workout title
    public string Title { get; set; } = string.Empty;

    // Optional workout notes/comments
    public string? Notes { get; set; }

    // Scheduled workout date/time
    public DateTime ScheduledDate { get; set; }
}
