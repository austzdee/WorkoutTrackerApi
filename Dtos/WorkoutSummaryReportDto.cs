namespace WorkoutTrackerApi.Dtos;

// Summary report for a user's workout activity
public class WorkoutSummaryReportDto
{
    public int TotalWorkouts { get; set; }

    public int TotalExercises { get; set; }

    public double TotalVolumeLifted { get; set; }

    public int UpcomingWorkouts { get; set; }
}