using Microsoft.Extensions.Logging;
using Model.Services.Interfaces;

namespace Model.Services;

public class UserService : IUserService {
    private readonly ILogger<UserService> _logger;
    private readonly SomeContext _context;

    public UserService(SomeContext context, ILogger<UserService> logger) {
        _logger = logger;
        _context = context;
    }
    public string[] GetAllUsers() {
        return _context.User.Select(u => u.Email).ToArray();
    }
}