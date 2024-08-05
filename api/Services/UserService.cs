using api.Interfaces;
using api.Models;
using api.Models.Enums;

namespace api.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await  _userRepository.GetAllAsync();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _userRepository.GetByIdAsync(id);
    }

    public async Task<User> CreateAsync(User userModel)
    {
        return await _userRepository.CreateAsync(userModel);
    }

    public void TransactionPolicy(User payer, decimal value)
    {
        if (payer.UserType == UserType.Merchant)
        {
            throw new UnauthorizedAccessException(
                "User of type MERCHANT is not allowed to perform fund transactions");
        }

        // TODO Create custom exception
        if (payer.Balance.CompareTo(value) < 0)
        {
            throw new Exception("Insufficient funds");
        }
    }
}
