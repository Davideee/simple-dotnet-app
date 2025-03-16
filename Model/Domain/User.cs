using System.Security.Cryptography;
using System.Text;

namespace Model.Domain;

public class User {
    public int Id { get; set; }
    public string Surname { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    public string Guid { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }

    public bool ValidatePassword(string password, byte[] passwordHash, byte[] passwordSalt) {
        using (var hmac = new HMACSHA256(passwordSalt)) {
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }
    }
}