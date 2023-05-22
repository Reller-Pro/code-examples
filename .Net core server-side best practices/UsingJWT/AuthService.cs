/// <summary>
/// Implementation of authentication service
/// </summary>
public class AuthService : IAuthService
{
    // Method to generate a JWT token for a user
    public string GenerateToken(User user)
    {
        // Create claims for the JWT token
        var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        // Create symmetric security key from appsettings.json
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]));

        // Create JWT token with claims and signing credentials
        var token = new JwtSecurityToken(
            issuer: Configuration["Jwt:Issuer"],
            audience: Configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(1),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

        // Return the JWT token string
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}