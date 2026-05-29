using System.ComponentModel.DataAnnotations;

namespace WorkoutTrackerApi.Dtos;

// Data required to create a workout plan
public class CreateWorkoutPlanDto
{
       [Required]
    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Notes { get; set; }

    [Required]
    public DateTime ScheduledDate { get; set; }
}



