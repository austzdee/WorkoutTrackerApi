using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkoutTrackerApi.Models;

// Represents a workout plan created by a user
public class WorkoutPlan
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? Notes { get; set; }

    public DateTime ScheduledDate { get; set; }

    public int UserId { get; set; }

    public User? User { get; set; }

    public List<Exercise> Exercises { get; set; } = new();
}