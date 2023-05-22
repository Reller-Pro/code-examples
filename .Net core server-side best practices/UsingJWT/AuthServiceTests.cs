[TestClass]
public class AuthServiceTests
{
    private Mock<IConfiguration> _configurationMock;
    private AuthService _authService;

    [TestInitialize]
    public void Setup()
    {
        _configurationMock = new Mock<IConfiguration>();
        _configurationMock.SetupGet(x => x["Jwt:Key"]).Returns("test_key");
        _configurationMock.SetupGet(x => x["Jwt:Issuer"]).Returns("test_issuer");
        _configurationMock.SetupGet(x => x["Jwt:Audience"]).Returns("test_audience");

        _authService = new AuthService(_configurationMock.Object);
    }

    [TestMethod]
    public void GenerateToken_Should_Return_Token_With_Valid_Claims()
    {
        // Arrange
        var user = new User { Id = "test_user_id", Email = "test_user_email@exampleNO LINKS" };

        // Act
        var token = _authService.GenerateToken(user);

        // Assert
        var handler = new JwtSecurityTokenHandler();
        var decodedToken = handler.ReadJwtToken(token);

        Assert.AreEqual(user.Id, decodedToken.Subject);
        Assert.AreEqual(user.Email, decodedToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.Email).Value);
        Assert.IsNotNull(decodedToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.Jti).Value);
    }
}