using api.Exceptions;
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

    public async Task WithdrawalAsync(int id, decimal amount)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if (user.Balance < amount)
        {
            throw new InvalidOperationException("Insufficient funds.");
        }

        user.Balance -= amount;
        await _userRepository.UpdateAsync(user);
    }

    public async Task DepositAsync(int id, decimal amount)
    {
        var user = await _userRepository.GetByIdAsync(id);
        user.Balance += amount;
        await _userRepository.UpdateAsync(user);    }

    public void TransactionPolicy(User payer, decimal value)
    {
        if (payer.UserType == UserType.Merchant)
        {
            throw new UnauthorizedAccessException(
                "User of type MERCHANT is not allowed to perform fund transactions");
        }
    }
}
