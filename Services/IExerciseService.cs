using WorkoutTrackerApi.Dtos;

namespace WorkoutTrackerApi.Services;

// Defines exercise business operations
public interface IExerciseService
{
    Task<ExerciseResponseDto> AddExerciseAsync(
        int userId,
        int workoutPlanId,
        CreateExerciseDto request);

    Task<IEnumerable<ExerciseResponseDto>?> GetExercisesAsync(
        int userId,
        int workoutPlanId);

    Task<ExerciseResponseDto?> GetExerciseByIdAsync(
        int userId,
        int workoutPlanId,
        int exerciseId);

    Task<ExerciseResponseDto?> UpdateExerciseAsync(
        int userId,
        int workoutPlanId,
        int exerciseId,
        UpdateExerciseDto request);

    Task<bool> DeleteExerciseAsync(
        int userId,
        int workoutPlanId,
        int exerciseId);
}