namespace WorkoutTrackerApi.Dtos;

// Authentication response returned to client
public class AuthResponseDto
{
    // JWT tokenl
    public string Token { get; set; } = string.Empty;

    // User email
    public string Email { get; set; } = string.Empty;

    // User full name
    public string FullName { get; set; } = string.Empty;
}