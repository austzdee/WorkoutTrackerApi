namespace WorkoutTrackerApi.Dtos;

// Data returned to the client for an exercise
public class ExerciseResponseDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int Sets { get; set; }

    public int Reps { get; set; }

    public double Weight { get; set; }
}