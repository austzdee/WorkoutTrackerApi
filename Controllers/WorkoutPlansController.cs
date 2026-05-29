using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WorkoutTrackerApi.Data;
using WorkoutTrackerApi.Dtos;
using WorkoutTrackerApi.Models;

namespace WorkoutTrackerApi.Controllers;

// Handles workout plan operations
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
    public async Task<ActionResult<WorkoutPlan>> CreateWorkout(
        CreateWorkoutPlanDto request)
    {
        // Extract logged-in user ID from JWT token
        var userId = int.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!
        );

        // Create new workout plan
        var workout = new WorkoutPlan
        {
            Title = request.Title,
            Notes = request.Notes,
            ScheduledDate = request.ScheduledDate,
            UserId = userId
        };

        _context.WorkoutPlans.Add(workout);

        await _context.SaveChangesAsync();

        return Ok(workout);
    }

    // GET: api/workoutplans
    [HttpGet]
    public async Task<ActionResult<IEnumerable<WorkoutPlan>>> GetWorkouts()
    {
        // Extract logged-in user ID
        var userId = int.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!
        );

        // Return ONLY workouts belonging to logged-in user
        var workouts = await _context.WorkoutPlans
            .Where(w => w.UserId == userId)
            .OrderBy(w => w.ScheduledDate)
            .ToListAsync();

        return Ok(workouts);
    }
}