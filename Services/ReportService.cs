using Microsoft.EntityFrameworkCore;
using WorkoutTrackerApi.Data;
using WorkoutTrackerApi.Dtos;

namespace WorkoutTrackerApi.Services;

// Handles workout reporting and analytics logic
public class ReportService : IReportService
{
    private readonly AppDbContext _context;

    public ReportService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<WorkoutSummaryReportDto> GetSummaryAsync(int userId)
    {
        var workoutIds = await _context.WorkoutPlans
            .AsNoTracking()
            .Where(w => w.UserId == userId)
            .Select(w => w.Id)
            .ToListAsync();

        var totalWorkouts = workoutIds.Count;

        var totalExercises = await _context.Exercises
            .AsNoTracking()
            .CountAsync(e => workoutIds.Contains(e.WorkoutPlanId));

        var totalVolumeLifted = await _context.Exercises
            .AsNoTracking()
            .Where(e => workoutIds.Contains(e.WorkoutPlanId))
            .SumAsync(e => e.Sets * e.Reps * e.Weight);

        var upcomingWorkouts = await _context.WorkoutPlans
            .AsNoTracking()
            .CountAsync(w =>
                w.UserId == userId &&
                w.ScheduledDate >= DateTime.UtcNow
            );

        return new WorkoutSummaryReportDto
        {
            TotalWorkouts = totalWorkouts,
            TotalExercises = totalExercises,
            TotalVolumeLifted = totalVolumeLifted,
            UpcomingWorkouts = upcomingWorkouts
        };
    }
}