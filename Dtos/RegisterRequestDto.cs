namespace WorkoutTrackerApi.Dtos;

// Data required when a user registers
public class RegisterRequestDto
{
    // User full name
    public string FullName { get; set; } = string.Empty;

    // User email
    public string Email { get; set; } = string.Empty;

    // Plain-text password from client
    public string Password { get; set; } = string.Empty;
}