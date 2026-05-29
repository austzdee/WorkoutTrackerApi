namespace WorkoutTrackerApi.Dtos;

// Data returned when fetching a workout plan
public class WorkoutPlanResponseDto
{
    // Workout ID
    public int Id { get; set; }

    // Workout title
    public string Title { get; set; } = string.Empty;

    // Optional workout notes/comments
    public string? Notes { get; set; }

    // Scheduled workout date/time
    public DateTime ScheduledDate { get; set; }
}
