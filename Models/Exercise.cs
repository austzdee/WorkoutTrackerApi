namespace WorkoutTrackerApi.Models;

// Represents an exercise inside a workout plan
public class Exercise
{
    // Primary Key
    public int Id { get; set; }

    // Exercise name
    public string Name { get; set; } = string.Empty;

    // Number of sets
    public int Sets { get; set; }

    // Number of repetitions
    public int Reps { get; set; }

    // Weight used for exercise
    public double Weight { get; set; }

    // Foreign Key:
    // Links exercise to workout plan
    public int WorkoutPlanId { get; set; }

    // Navigation Property:
    // Exercise belongs to one workout plan
    public WorkoutPlan? WorkoutPlan { get; set; }
}