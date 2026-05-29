namespace WorkoutTrackerApi.Dtos;
// Data required to update a workout plan
public class UpdateWorkoutPlanDto
{
    // Update Workout title
    public string Title { get; set; } = string.Empty;

    // Optional workout notes/comments
    public string? Notes { get; set; }

    // Scheduled workout date/time
    public DateTime ScheduledDate { get; set; }
}