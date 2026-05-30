using WorkoutTrackerApi.Dtos;

namespace WorkoutTrackerApi.Services;

// Defines reporting and analytics operations
public interface IReportService
{
    Task<WorkoutSummaryReportDto> GetSummaryAsync(int userId);
}