using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WorkoutTrackerApi.Data;
using WorkoutTrackerApi.Dtos;
using WorkoutTrackerApi.Models;

namespace WorkoutTrackerApi.Controllers;

// Handles authenticated workout plan operations
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class WorkoutPlansController : ControllerBase
{
    private readonly AppDbContext _context;

    public WorkoutPlansController(AppDbContext context)
    {
        _context = context;
    }

    // POST: api/workoutplans
    [HttpPost]
    public async Task<ActionResult<WorkoutPlanResponseDto>> CreateWorkout(
        CreateWorkoutPlanDto request)
    {
        var userId = GetCurrentUserId();

        var workout = new WorkoutPlan
        {
            Title = request.Title.Trim(),
            Notes = request.Notes?.Trim(),
            ScheduledDate = request.ScheduledDate,
            UserId = userId
        };

        _context.WorkoutPlans.Add(workout);
        await _context.SaveChangesAsync();

        var response = MapToWorkoutPlanResponse(workout);

        return CreatedAtAction(
            nameof(GetWorkoutById),
            new { id = workout.Id },
            response
        );
    }

    // GET: api/workoutplans
    [HttpGet]
    public async Task<ActionResult<IEnumerable<WorkoutPlanResponseDto>>> GetWorkouts()
    {
        var userId = GetCurrentUserId();

        var workouts = await _context.WorkoutPlans
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

        return Ok(workouts);
    }

    // GET: api/workoutplans/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<WorkoutPlanResponseDto>> GetWorkoutById(int id)
    {
        var userId = GetCurrentUserId();

        var workout = await _context.WorkoutPlans
            .AsNoTracking()
            .Where(w => w.Id == id && w.UserId == userId)
            .Select(w => new WorkoutPlanResponseDto
            {
                Id = w.Id,
                Title = w.Title,
                Notes = w.Notes,
                ScheduledDate = w.ScheduledDate
            })
            .FirstOrDefaultAsync();

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

        var workout = await _context.WorkoutPlans
            .FirstOrDefaultAsync(w => w.Id == id && w.UserId == userId);

        if (workout is null)
        {
            return NotFound("Workout plan not found.");
        }

        workout.Title = request.Title.Trim();
        workout.Notes = request.Notes?.Trim();
        workout.ScheduledDate = request.ScheduledDate;

        await _context.SaveChangesAsync();

        return Ok(MapToWorkoutPlanResponse(workout));
    }

    // DELETE: api/workoutplans/{id}
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteWorkout(int id)
    {
        var userId = GetCurrentUserId();

        var workout = await _context.WorkoutPlans
            .FirstOrDefaultAsync(w => w.Id == id && w.UserId == userId);

        if (workout is null)
        {
            return NotFound("Workout plan not found.");
        }

        _context.WorkoutPlans.Remove(workout);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // Extracts the authenticated user ID from JWT claims
    private int GetCurrentUserId()
    {
        return int.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!
        );
    }

    // Maps database entity to response DTO
    private static WorkoutPlanResponseDto MapToWorkoutPlanResponse(WorkoutPlan workout)
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