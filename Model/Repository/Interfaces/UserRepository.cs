using Model.Domain;

namespace Model.Repository.Interfaces;

public interface IUserRepository {
    Task<User?> GetUserByEmailAsync(string email);
    Task<string[]> GetAllUsersAsync();
    Task AddUserAsync(User? user);
}