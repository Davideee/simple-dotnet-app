using Model.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Model.Domain;
using System.Text;
using System.Security.Cryptography;
using Model.Contract;
using Model.Repository.Interfaces;

namespace Model.Services;

public class UserService : IUserService {
    private readonly IUserRepository _userRepository;
    private readonly ILogger<UserService> _logger;

    public UserService(IUserRepository userRepository, ILogger<UserService> logger) {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<string[]> GetAllUsersAsync() {
        return await _userRepository.GetAllUsersAsync();
    }

    public async Task RegisterUserAsync(Register registerData) {
        if (await _userRepository.GetUserByEmailAsync(registerData.Email) != null)
            throw new Exception("Email already in use.");

        CreatePasswordHash(registerData.Password, out var passwordHash, out var passwordSalt);

        var user = new User {
            Surname = registerData.Surname,
            Lastname = registerData.Lastname,
            Email = registerData.Email,
            Guid = Guid.NewGuid().ToString(),
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
        };

        await _userRepository.AddUserAsync(user);
    }

    public async Task<User> LoginUserAsync(Login loginData) {
        var user = await _userRepository.GetUserByEmailAsync(loginData.Email);
        if (user == null || !user.ValidatePassword(loginData.Password, user.PasswordHash, user.PasswordSalt))
            throw new Exception("Invalid login attempt.");

        return user;
    }

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt) {
        using (var hmac = new HMACSHA256()) {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }
}