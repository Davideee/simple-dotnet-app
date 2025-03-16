using Model.Contract;
using Model.Domain;

namespace Model.Services.Interfaces;

public interface IUserService {
    Task<string[]> GetAllUsersAsync();
    Task RegisterUserAsync(Register registerData);
    Task<User> LoginUserAsync(Login loginData);
}