/// <summary>
/// Interface for authentication service
/// </summary>
public interface IAuthService
{
    // Method to generate a JWT token for a user
    public string GenerateToken(User user);
}