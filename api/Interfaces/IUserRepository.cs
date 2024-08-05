using api.Models;
using api.Models.Enums;

namespace api.Interfaces;

public interface IUserRepository
{
    Task<List<User>> GetAllAsync();
}
