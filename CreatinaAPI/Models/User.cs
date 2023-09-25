using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace CreatinaAPI.Models;

public class User
{
    [Key]
    public int UserId { get; set; }
    [Required]
    [StringLength(64)]
    public string Name { get; set; }
    [Required]
    [StringLength(64)]
    public string Email { get; set; }
    [Required]
    [StringLength(128)]
    public string Password { get; set; }
    public string PasswordHash { get; set; }
    public Creatine Creatine { get; set; }

    public void SetPassword(string password)
    {
        byte[] salt = new byte[128 / 8];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        string hashed = Convert.ToBase64String(
            KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 1000,
                numBytesRequested: 256 / 8
                )
            );

        PasswordHash = $"{Convert.ToBase64String(salt)}:{hashed}";
    }

    public bool VerifyPassword(string password)
    {
        string[] parts = PasswordHash.Split(':');
        if (parts.Length != 2)
        {
            return false;
        }
        byte[] salt = Convert.FromBase64String(parts[0]);
        string savedHash = parts[1];

        string hashed = Convert.ToBase64String(
            KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 1000,
                numBytesRequested: 256 / 8
            )
        );

        return savedHash == hashed;
    }
}
