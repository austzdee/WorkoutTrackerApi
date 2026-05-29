using System.ComponentModel.DataAnnotations;

namespace WorkoutTrackerApi.Dtos;

// Data required to add an exercise to a workout plan
public class CreateExerciseDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Range(1, 100)]
    public int Sets { get; set; }

    [Range(1, 1000)]
    public int Reps { get; set; }

    [Range(0, 1000)]
    public double Weight { get; set; }
}