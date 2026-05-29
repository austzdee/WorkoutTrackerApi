using System.ComponentModel.DataAnnotations;

namespace WorkoutTrackerApi.Dtos;

// Data required to update a workout plan
public class UpdateWorkoutPlanDto
{
    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Notes { get; set; }

    [Required]
    public DateTime ScheduledDate { get; set; }
}