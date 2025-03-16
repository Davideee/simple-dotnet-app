using Microsoft.EntityFrameworkCore;
using Model.Domain;
using Model.Repository.Interfaces;

namespace Model.Repository;

public class UserRepository : IUserRepository {
    private readonly UserContext _context;

    public UserRepository(UserContext context) {
        _context = context;
    }

    public async Task<User?> GetUserByEmailAsync(string email) {
        return await _context.User.FirstOrDefaultAsync(u => u != null && u.Email == email);
    }

    public async Task<string[]> GetAllUsersAsync() {
        return await _context.User.Select(u => u.Email).ToArrayAsync();
    }


    public async Task AddUserAsync(User? user) {
        _context.User.Add(user);
        await _context.SaveChangesAsync();
    }
}