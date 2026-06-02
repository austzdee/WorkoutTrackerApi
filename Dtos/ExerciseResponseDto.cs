namespace WorkoutTrackerApi.Dtos;

// Data returned to the client for an exercise.
public class ExerciseResponseDto
{
    // Unique identifier for the exercise.
    public int Id { get; set; }

    // Exercise name, for example: Bench Press, Squat, Deadlift.
    public string Name { get; set; } = string.Empty;

    // Number of sets assigned to the exercise.
    public int Sets { get; set; }

    // Number of repetitions per set.
    public int Reps { get; set; }

    // Weight used for the exercise.
    public double Weight { get; set; }

    // Optional note or progress comment for the exercise.
   
}