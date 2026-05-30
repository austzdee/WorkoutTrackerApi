using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WorkoutTrackerApi.Dtos;
using WorkoutTrackerApi.Services;

namespace WorkoutTrackerApi.Controllers;

// Handles authenticated workout plan operations
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class WorkoutPlansController : ControllerBase
{
    private readonly IWorkoutPlanService _workoutPlanService;

    public WorkoutPlansController(IWorkoutPlanService workoutPlanService)
    {
        _workoutPlanService = workoutPlanService;
    }

    // POST: api/workoutplans
    [HttpPost]
    public async Task<ActionResult<WorkoutPlanResponseDto>> CreateWorkout(
        CreateWorkoutPlanDto request)
    {
        var userId = GetCurrentUserId();

        var workout = await _workoutPlanService.CreateWorkoutAsync(
            userId,
            request
        );

        return CreatedAtAction(
            nameof(GetWorkoutById),
            new { id = workout.Id },
            workout
        );
    }

    // GET: api/workoutplans
    [HttpGet]
    public async Task<ActionResult<IEnumerable<WorkoutPlanResponseDto>>> GetWorkouts()
    {
        var userId = GetCurrentUserId();

        var workouts = await _workoutPlanService.GetWorkoutsAsync(userId);

        return Ok(workouts);
    }

    // GET: api/workoutplans/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<WorkoutPlanResponseDto>> GetWorkoutById(int id)
    {
        var userId = GetCurrentUserId();

        var workout = await _workoutPlanService.GetWorkoutByIdAsync(
            userId,
            id
        );

        if (workout is null)
        {
            return NotFound("Workout plan not found.");
        }

        return Ok(workout);
    }

    // PUT: api/workoutplans/{id}
    [HttpPut("{id:int}")]
    public async Task<ActionResult<WorkoutPlanResponseDto>> UpdateWorkout(
        int id,
        UpdateWorkoutPlanDto request)
    {
        var userId = GetCurrentUserId();

        var workout = await _workoutPlanService.UpdateWorkoutAsync(
            userId,
            id,
            request
        );

        if (workout is null)
        {
            return NotFound("Workout plan not found.");
        }

        return Ok(workout);
    }

    // DELETE: api/workoutplans/{id}
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteWorkout(int id)
    {
        var userId = GetCurrentUserId();

        var deleted = await _workoutPlanService.DeleteWorkoutAsync(
            userId,
            id
        );

        if (!deleted)
        {
            return NotFound("Workout plan not found.");
        }

        return NoContent();
    }

    // Extracts authenticated user ID from JWT claims
    private int GetCurrentUserId()
    {
        return int.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!
        );
    }
}