namespace WorkoutTrackerApi.Dtos;

// Data required when a user logs in
public class LoginRequestDto
{
    // User email
    public string Email { get; set; } = string.Empty;

    // Plain-text password
    public string Password { get; set; } = string.Empty;
}