using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

[TestClass]
public class PasswordHelperTests
{
    [TestMethod]
    public void HashPassword_ShouldReturnDifferentValues_ForTwoIdenticalPasswords()
    {
        // Arrange
        var salt = PasswordHelper.GetRandomSalt();
        const string password = "password";

        // Act
        var hashedPassword1 = PasswordHelper.HashPassword(password, salt);
        var hashedPassword2 = PasswordHelper.HashPassword(password, salt);

        // Assert
        Assert.AreNotEqual(hashedPassword1, hashedPassword2, "Hashed passwords should not be equal for same password");
    }

    [TestMethod]
    public void VerifyPassword_ShouldReturnTrue_ForCorrectPassword()
    {
        // Arrange
        var salt = PasswordHelper.GetRandomSalt();
        const string password = "password";
        var hashedPassword = PasswordHelper.HashPassword(password, salt);

        // Act
        var result = PasswordHelper.VerifyPassword(password, salt, hashedPassword);

        // Assert
        Assert.IsTrue(result, "Password verification failed for the correct password");
    }

    [TestMethod]
    public void VerifyPassword_ShouldReturnFalse_ForIncorrectPassword()
    {
        // Arrange
        var salt = PasswordHelper.GetRandomSalt();
        const string password = "password";
        var hashedPassword = PasswordHelper.HashPassword(password, salt);
        const string incorrectPassword = "incorrectpassword";

        // Act
        var result = PasswordHelper.VerifyPassword(incorrectPassword, salt, hashedPassword);

        // Assert
        Assert.IsFalse(result, "Password verification passed for an incorrect password");
    }

    [TestMethod]
    public void HashPassword_And_VerifyPassword_ShouldWork_WithMoq()
    {
        // Arrange
        const string password = "password";
        var salt = PasswordHelper.GetRandomSalt();

        // Create a mock UserRepository object
        var mockUserRepository = new Mock<IUserRepository>();
        mockUserRepository.Setup(repo => repo.CreateUser(
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .Returns((string email, string passwordHash, string salt) => new User
            {
                Email = email,
                PasswordHash = passwordHash,
                Salt = salt
            });

        // Act
        var newUser = mockUserRepository.Object.CreateUser("test@test.com",
            PasswordHelper.HashPassword(password, salt), salt);
        var result = PasswordHelper.VerifyPassword(password, newUser.Salt, newUser.PasswordHash);

        // Assert
        Assert.IsTrue(result, "Password verification failed for the created user");
    }
}