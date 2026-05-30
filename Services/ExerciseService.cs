using Microsoft.EntityFrameworkCore;
using WorkoutTrackerApi.Data;
using WorkoutTrackerApi.Dtos;
using WorkoutTrackerApi.Models;

namespace WorkoutTrackerApi.Services;

// Handles exercise business logic
public class ExerciseService : IExerciseService
{
    private readonly AppDbContext _context;
    private readonly ILogger<ExerciseService> _logger;

    public ExerciseService(
        AppDbContext context,
        ILogger<ExerciseService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ExerciseResponseDto> AddExerciseAsync(
        int userId,
        int workoutPlanId,
        CreateExerciseDto request)
    {
        var workoutExists = await _context.WorkoutPlans
            .AnyAsync(w => w.Id == workoutPlanId && w.UserId == userId);

        if (!workoutExists)
        {
            throw new KeyNotFoundException("Workout plan not found.");
        }

        var exercise = new Exercise
        {
            Name = request.Name.Trim(),
            Sets = request.Sets,
            Reps = request.Reps,
            Weight = request.Weight,
            WorkoutPlanId = workoutPlanId
        };

        _context.Exercises.Add(exercise);
        await _context.SaveChangesAsync();

        _logger.LogInformation(
            "Exercise {ExerciseId} added to workout {WorkoutPlanId} for user {UserId}",
            exercise.Id,
            workoutPlanId,
            userId
        );

        return MapToResponse(exercise);
    }

    public async Task<IEnumerable<ExerciseResponseDto>?> GetExercisesAsync(
        int userId,
        int workoutPlanId)
    {
        var workoutExists = await _context.WorkoutPlans
            .AnyAsync(w => w.Id == workoutPlanId && w.UserId == userId);

        if (!workoutExists)
        {
            return null;
        }

        return await _context.Exercises
            .AsNoTracking()
            .Where(e => e.WorkoutPlanId == workoutPlanId)
            .Select(e => new ExerciseResponseDto
            {
                Id = e.Id,
                Name = e.Name,
                Sets = e.Sets,
                Reps = e.Reps,
                Weight = e.Weight
            })
            .ToListAsync();
    }

    public async Task<ExerciseResponseDto?> GetExerciseByIdAsync(
        int userId,
        int workoutPlanId,
        int exerciseId)
    {
        var workoutExists = await _context.WorkoutPlans
            .AnyAsync(w => w.Id == workoutPlanId && w.UserId == userId);

        if (!workoutExists)
        {
            return null;
        }

        return await _context.Exercises
            .AsNoTracking()
            .Where(e => e.Id == exerciseId && e.WorkoutPlanId == workoutPlanId)
            .Select(e => new ExerciseResponseDto
            {
                Id = e.Id,
                Name = e.Name,
                Sets = e.Sets,
                Reps = e.Reps,
                Weight = e.Weight
            })
            .FirstOrDefaultAsync();
    }

    public async Task<ExerciseResponseDto?> UpdateExerciseAsync(
        int userId,
        int workoutPlanId,
        int exerciseId,
        UpdateExerciseDto request)
    {
        var workoutExists = await _context.WorkoutPlans
            .AnyAsync(w => w.Id == workoutPlanId && w.UserId == userId);

        if (!workoutExists)
        {
            return null;
        }

        var exercise = await _context.Exercises
            .FirstOrDefaultAsync(e =>
                e.Id == exerciseId &&
                e.WorkoutPlanId == workoutPlanId);

        if (exercise is null)
        {
            return null;
        }

        exercise.Name = request.Name.Trim();
        exercise.Sets = request.Sets;
        exercise.Reps = request.Reps;
        exercise.Weight = request.Weight;

        await _context.SaveChangesAsync();

        return MapToResponse(exercise);
    }

    public async Task<bool> DeleteExerciseAsync(
        int userId,
        int workoutPlanId,
        int exerciseId)
    {
        var workoutExists = await _context.WorkoutPlans
            .AnyAsync(w => w.Id == workoutPlanId && w.UserId == userId);

        if (!workoutExists)
        {
            return false;
        }

        var exercise = await _context.Exercises
            .FirstOrDefaultAsync(e =>
                e.Id == exerciseId &&
                e.WorkoutPlanId == workoutPlanId);

        if (exercise is null)
        {
            return false;
        }

        _context.Exercises.Remove(exercise);
        await _context.SaveChangesAsync();

        _logger.LogWarning(
            "Exercise {ExerciseId} deleted from workout {WorkoutPlanId} by user {UserId}",
            exerciseId,
            workoutPlanId,
            userId
        );

        return true;
    }

    private static ExerciseResponseDto MapToResponse(Exercise exercise)
    {
        return new ExerciseResponseDto
        {
            Id = exercise.Id,
            Name = exercise.Name,
            Sets = exercise.Sets,
            Reps = exercise.Reps,
            Weight = exercise.Weight
        };
    }
}