[TestClass]
public class AuthorizationControllerTests
{
    private Mock<IAuthService> _authServiceMock;
    private AuthorizationController _controller;

    [TestInitialize]
    public void Setup()
    {
        _authServiceMock = new Mock<IAuthService>();
        _controller = new AuthorizationController(_authServiceMock.Object);
    }

    [TestMethod]
    public void Login_ValidCredentials_ReturnsToken()
    {
        // Arrange
        var expectedToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c";
        var loginModel = new LoginModel { Username = "testuser", Password = "password" };
        var user = new User { Id = 1, Username = "testuser", Role = "user" };
        _authServiceMock.Setup(s => s.Login(loginModel)).Returns(user);
        _authServiceMock.Setup(s => s.GenerateToken(user)).Returns(expectedToken);

        // Act
        var result = _controller.Login(loginModel) as JsonResult;

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(expectedToken, result.Value);
    }

    [TestMethod]
    public void Login_InvalidCredentials_ReturnsUnauthorized()
    {
        // Arrange
        var loginModel = new LoginModel { Username = "testuser", Password = "wrongpassword" };
        _authServiceMock.Setup(s => s.Login(loginModel)).Returns((User)null);

        // Act
        var result = _controller.Login(loginModel) as UnauthorizedResult;

        // Assert
        Assert.IsNotNull(result);
    }
}