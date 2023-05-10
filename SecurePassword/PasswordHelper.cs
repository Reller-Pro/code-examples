using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;

/// <summary>
/// This class will be responsible for generating and verifying hashed passwords using the PBKDF2 algorithm.
/// </summary>
public static class PasswordHelper
{
    // Create a random salt value for password hashing
    private const int SaltSize = 128 / 8; // 128 bits / 8 bits per byte = 16 bytes
    private const int IterationCount = 10000;
    private const int HashSize = 256 / 8; // 256 bits / 8 bits per byte = 32 bytes

    public static string GetRandomSalt()
    {
        var salt = new byte[SaltSize];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(salt);

        return Convert.ToBase64String(salt);
    }

    // Generate a hash value for the password using the salt
    public static string HashPassword(string password, string salt)
    {
        var saltBytes = Convert.FromBase64String(salt);

        var hashBytes = KeyDerivation.Pbkdf2(
            password: password,
            salt: saltBytes,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: IterationCount,
            numBytesRequested: HashSize);

        return Convert.ToBase64String(hashBytes);
    }

    // Verify if the password and hash match using the salt
    public static bool VerifyPassword(string password, string salt, string hash)
    {
        var hashedPassword = HashPassword(password, salt);

        return (hash == hashedPassword);
    }
}