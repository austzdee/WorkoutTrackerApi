using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WorkoutTrackerApi.Dtos;
using WorkoutTrackerApi.Services;

namespace WorkoutTrackerApi.Controllers;

// Handles authenticated reporting endpoints
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ReportsController : ControllerBase
{
    private readonly IReportService _reportService;

    public ReportsController(IReportService reportService)
    {
        _reportService = reportService;
    }

    // GET: api/reports/summary
    [HttpGet("summary")]
    public async Task<ActionResult<WorkoutSummaryReportDto>> GetSummary()
    {
        var userId = GetCurrentUserId();

        var report = await _reportService.GetSummaryAsync(userId);

        return Ok(report);
    }

    // Extract authenticated user ID from JWT claims
    private int GetCurrentUserId()
    {
        return int.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!
        );
    }
}