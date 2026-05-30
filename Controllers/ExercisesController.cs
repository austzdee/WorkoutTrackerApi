using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WorkoutTrackerApi.Dtos;
using WorkoutTrackerApi.Services;

namespace WorkoutTrackerApi.Controllers;

// Handles exercise operations inside workout plans
[ApiController]
[Route("api/workoutplans/{workoutPlanId:int}/exercises")]
[Authorize]
public class ExercisesController : ControllerBase
{
    private readonly IExerciseService _exerciseService;

    public ExercisesController(IExerciseService exerciseService)
    {
        _exerciseService = exerciseService;
    }

    // POST: api/workoutplans/{workoutPlanId}/exercises
    [HttpPost]
    public async Task<ActionResult<ExerciseResponseDto>> AddExercise(
        int workoutPlanId,
        CreateExerciseDto request)
    {
        var userId = GetCurrentUserId();

        var exercise = await _exerciseService.AddExerciseAsync(
            userId,
            workoutPlanId,
            request
        );

        return Ok(exercise);
    }

    // GET: api/workoutplans/{workoutPlanId}/exercises
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ExerciseResponseDto>>> GetExercises(
        int workoutPlanId)
    {
        var userId = GetCurrentUserId();

        var exercises = await _exerciseService.GetExercisesAsync(
            userId,
            workoutPlanId
        );

        if (exercises is null)
        {
            return NotFound("Workout plan not found.");
        }

        return Ok(exercises);
    }

    // GET: api/workoutplans/{workoutPlanId}/exercises/{exerciseId}
    [HttpGet("{exerciseId:int}")]
    public async Task<ActionResult<ExerciseResponseDto>> GetExerciseById(
        int workoutPlanId,
        int exerciseId)
    {
        var userId = GetCurrentUserId();

        var exercise = await _exerciseService.GetExerciseByIdAsync(
            userId,
            workoutPlanId,
            exerciseId
        );

        if (exercise is null)
        {
            return NotFound("Exercise not found.");
        }

        return Ok(exercise);
    }

    // PUT: api/workoutplans/{workoutPlanId}/exercises/{exerciseId}
    [HttpPut("{exerciseId:int}")]
    public async Task<ActionResult<ExerciseResponseDto>> UpdateExercise(
        int workoutPlanId,
        int exerciseId,
        UpdateExerciseDto request)
    {
        var userId = GetCurrentUserId();

        var exercise = await _exerciseService.UpdateExerciseAsync(
            userId,
            workoutPlanId,
            exerciseId,
            request
        );

        if (exercise is null)
        {
            return NotFound("Exercise not found.");
        }

        return Ok(exercise);
    }

    // DELETE: api/workoutplans/{workoutPlanId}/exercises/{exerciseId}
    [HttpDelete("{exerciseId:int}")]
    public async Task<IActionResult> DeleteExercise(
        int workoutPlanId,
        int exerciseId)
    {
        var userId = GetCurrentUserId();

        var deleted = await _exerciseService.DeleteExerciseAsync(
            userId,
            workoutPlanId,
            exerciseId
        );

        if (!deleted)
        {
            return NotFound("Exercise not found.");
        }

        return NoContent();
    }

    // Extract authenticated user ID from JWT claims
    private int GetCurrentUserId()
    {
        return int.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!
        );
    }
}