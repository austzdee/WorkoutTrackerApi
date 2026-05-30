using WorkoutTrackerApi.Dtos;

namespace WorkoutTrackerApi.Services;

// Defines workout plan business operations
public interface IWorkoutPlanService
{
    Task<WorkoutPlanResponseDto> CreateWorkoutAsync(
        int userId,
        CreateWorkoutPlanDto request);

    Task<IEnumerable<WorkoutPlanResponseDto>> GetWorkoutsAsync(int userId);

    Task<WorkoutPlanResponseDto?> GetWorkoutByIdAsync(
        int userId,
        int workoutId);

    Task<WorkoutPlanResponseDto?> UpdateWorkoutAsync(
        int userId,
        int workoutId,
        UpdateWorkoutPlanDto request);

    Task<bool> DeleteWorkoutAsync(
        int userId,
        int workoutId);
}