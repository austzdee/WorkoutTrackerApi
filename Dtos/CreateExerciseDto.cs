namespace WorkoutTrackerApi.Dtos;

// Data required to create an exercise
public class CreateExerciseDto
{
      public string Name { get; set; } = string.Empty;

    public int Sets { get; set; }

    public int Reps { get; set; }

    public double Weight { get; set; }
}



