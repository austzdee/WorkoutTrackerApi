using Microsoft.EntityFrameworkCore;
using WorkoutTrackerApi.Data;
using WorkoutTrackerApi.Dtos;
using WorkoutTrackerApi.Models;

namespace WorkoutTrackerApi.Services;

// Handles workout plan business logic
public class WorkoutPlanService : IWorkoutPlanService
{
    private readonly AppDbContext _context;
    private readonly ILogger<WorkoutPlanService> _logger;

    public WorkoutPlanService(
        AppDbContext context,
        ILogger<WorkoutPlanService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<WorkoutPlanResponseDto> CreateWorkoutAsync(
        int userId,
        CreateWorkoutPlanDto request)
    {
        _logger.LogInformation(
            "Creating workout plan for user {UserId}",
            userId
        );

        var workout = new WorkoutPlan
        {
            Title = request.Title.Trim(),
            Notes = request.Notes?.Trim(),
            ScheduledDate = request.ScheduledDate,
            UserId = userId
        };

        _context.WorkoutPlans.Add(workout);
        await _context.SaveChangesAsync();

        _logger.LogInformation(
            "Workout plan {WorkoutId} created successfully for user {UserId}",
            workout.Id,
            userId
        );

        return MapToResponse(workout);
    }

    public async Task<IEnumerable<WorkoutPlanResponseDto>> GetWorkoutsAsync(int userId)
    {
        _logger.LogInformation(
            "Fetching workout plans for user {UserId}",
            userId
        );

        return await _context.WorkoutPlans
            .AsNoTracking()
            .Where(w => w.UserId == userId)
            .OrderBy(w => w.ScheduledDate)
            .Select(w => new WorkoutPlanResponseDto
            {
                Id = w.Id,
                Title = w.Title,
                Notes = w.Notes,
                ScheduledDate = w.ScheduledDate
            })
            .ToListAsync();
    }

    public async Task<WorkoutPlanResponseDto?> GetWorkoutByIdAsync(
        int userId,
        int workoutId)
    {
        _logger.LogInformation(
            "Fetching workout plan {WorkoutId} for user {UserId}",
            workoutId,
            userId
        );

        return await _context.WorkoutPlans
            .AsNoTracking()
            .Where(w => w.Id == workoutId && w.UserId == userId)
            .Select(w => new WorkoutPlanResponseDto
            {
                Id = w.Id,
                Title = w.Title,
                Notes = w.Notes,
                ScheduledDate = w.ScheduledDate
            })
            .FirstOrDefaultAsync();
    }

    public async Task<WorkoutPlanResponseDto?> UpdateWorkoutAsync(
        int userId,
        int workoutId,
        UpdateWorkoutPlanDto request)
    {
        var workout = await _context.WorkoutPlans
            .FirstOrDefaultAsync(w => w.Id == workoutId && w.UserId == userId);

        if (workout is null)
        {
            _logger.LogWarning(
                "Update failed. Workout plan {WorkoutId} not found for user {UserId}",
                workoutId,
                userId
            );

            return null;
        }

        workout.Title = request.Title.Trim();
        workout.Notes = request.Notes?.Trim();
        workout.ScheduledDate = request.ScheduledDate;

        await _context.SaveChangesAsync();

        _logger.LogInformation(
            "Workout plan {WorkoutId} updated successfully for user {UserId}",
            workoutId,
            userId
        );

        return MapToResponse(workout);
    }

    public async Task<bool> DeleteWorkoutAsync(int userId, int workoutId)
    {
        var workout = await _context.WorkoutPlans
            .FirstOrDefaultAsync(w => w.Id == workoutId && w.UserId == userId);

        if (workout is null)
        {
            _logger.LogWarning(
                "Delete failed. Workout plan {WorkoutId} not found for user {UserId}",
                workoutId,
                userId
            );

            return false;
        }

        _logger.LogWarning(
            "Deleting workout plan {WorkoutId} for user {UserId}",
            workoutId,
            userId
        );

        _context.WorkoutPlans.Remove(workout);
        await _context.SaveChangesAsync();

        _logger.LogInformation(
            "Workout plan {WorkoutId} deleted successfully for user {UserId}",
            workoutId,
            userId
        );

        return true;
    }

    private static WorkoutPlanResponseDto MapToResponse(WorkoutPlan workout)
    {
        return new WorkoutPlanResponseDto
        {
            Id = workout.Id,
            Title = workout.Title,
            Notes = workout.Notes,
            ScheduledDate = workout.ScheduledDate
        };
    }
}