using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WorkoutTrackerApi.Data;
using WorkoutTrackerApi.Dtos;
using WorkoutTrackerApi.Models;

namespace WorkoutTrackerApi.Controllers;

// Handles exercise operations inside workout plans
[ApiController]
[Route("api/workoutplans/{workoutPlanId:int}/exercises")]
[Authorize]
public class ExercisesController : ControllerBase
{
    private readonly AppDbContext _context;

    public ExercisesController(AppDbContext context)
    {
        _context = context;
    }

    // POST: api/workoutplans/{workoutPlanId}/exercises
    [HttpPost]
    public async Task<ActionResult<ExerciseResponseDto>> AddExercise(
        int workoutPlanId,
        CreateExerciseDto request)
    {
        var userId = GetCurrentUserId();

        var workoutExists = await _context.WorkoutPlans
            .AnyAsync(w => w.Id == workoutPlanId && w.UserId == userId);

        if (!workoutExists)
        {
            return NotFound("Workout plan not found.");
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

        return Ok(MapToExerciseResponse(exercise));
    }

    // GET: api/workoutplans/{workoutPlanId}/exercises
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ExerciseResponseDto>>> GetExercises(
        int workoutPlanId)
    {
        var userId = GetCurrentUserId();

        var workoutExists = await _context.WorkoutPlans
            .AnyAsync(w => w.Id == workoutPlanId && w.UserId == userId);

        if (!workoutExists)
        {
            return NotFound("Workout plan not found.");
        }

        var exercises = await _context.Exercises
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

        return Ok(exercises);
    }

    private int GetCurrentUserId()
    {
        return int.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!
        );
    }

    private static ExerciseResponseDto MapToExerciseResponse(Exercise exercise)
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