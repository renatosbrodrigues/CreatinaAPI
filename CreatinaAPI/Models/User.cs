using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text.Json.Serialization;
using BCrypt;
using BCrypt.Net;

namespace CreatinaAPI.Models;

public class User
{
    [Key]
    [JsonIgnore]
    public int UserId { get; set; }
    [Required]
    [StringLength(64)]
    public string Name { get; set; }
    [Required]
    [StringLength(64)]
    public string Email { get; set; }
    [Required(ErrorMessage ="É nescessário criar uma senha")]
    [StringLength(128)]
    [NotMapped]
    public string Password {  get; set; }
    public string PasswordHash { get; set; }
    [JsonIgnore]
    public Creatine? Creatine { get; set; }

    public void SetPassword(string password)
    {
        PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool VerifyPassword(string password)
    {
        return BCrypt.Net.BCrypt.Verify(password, PasswordHash);
    }
}
