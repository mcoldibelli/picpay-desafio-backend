using api.Models;

namespace api.Interfaces;

public interface IUserRepository
{
    Task<List<User>> GetAllAsync();
    Task<User?> GetByIdAsync(int id);
    Task<User> CreateAsync(User userModel);
    Task<User> UpdateAsync(User userModel);
}
