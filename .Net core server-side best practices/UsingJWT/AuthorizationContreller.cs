/// <summary>
/// Authorization controller for handling user login
/// </summary>
public class AuthorizationController : Controller
{
    private readonly IAuthService _authService;

    // Constructor that injects the authentication service
    public AuthorizationController(IAuthService authService)
    {
        _authService = authService;
    }

    // Login method for generating a JWT token for a user
    [HttpPost]
    public IActionResult Login(LoginModel loginModel)
    {
        // Authenticate the user and retrieve their user object
        var user = _authService.Login(loginModel);
        // Generate a JWT token for the user
        var token = _authService.GenerateToken(user);
        // Return the JWT token string in a JSON response
        return new JsonResult(token);
    }
}