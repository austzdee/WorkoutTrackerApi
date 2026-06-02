namespace WorkoutTrackerApi.Dtos;

// Data returned when fetching a workout plan
public class WorkoutPlanResponseDto
{
    // Unique identifier for the workout plan.
    public int Id { get; set; }

    // Display title for the workout plan.
    public string Title { get; set; } = string.Empty;

    // Optional notes or comments attached to the workout plan.
    public string? Notes { get; set; }

    // Date and time when the workout is scheduled.
    public DateTime ScheduledDate { get; set; }

    // Exercises that belong to this workout plan.
    public List<ExerciseResponseDto> Exercises { get; set; } = [];
    
}
