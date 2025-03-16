using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.Domain;
using Web.Controllers.Contract;

namespace Web.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthorizationController : ControllerBase {
    private readonly UserContext _context;

    public AuthorizationController(UserContext context) {
        _context = context;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto request) {
        if (await _context.User.AnyAsync(u => u.Email == request.Email))
            return BadRequest("E-Mail already exists");

        CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

        var user = new User {
            Surname = request.Surname,
            Lastname = request.Lastname,
            Email = request.Email,
            Guid = Guid.NewGuid().ToString(),
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
        };

        _context.User.Add(user);
        await _context.SaveChangesAsync();

        return Ok("Registered new User");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto request) {
        var user = await _context.User.SingleOrDefaultAsync(u => u.Email == request.Email);
        if (user == null || !VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            return Unauthorized("Wrong E-Mail or Password.");

        await _context.SaveChangesAsync();

        return Ok(new { message = "Login successful", userId = user.Id });
    }

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt) {
        using var hmac = new HMACSHA256();
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }

    private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt) {
        using var hmac = new HMACSHA256(storedSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return computedHash.SequenceEqual(storedHash);
    }
}