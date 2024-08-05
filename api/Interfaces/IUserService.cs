using api.Models;

namespace api.Interfaces;

public interface IUserService
{
    Task<List<User>> GetAllAsync();
    Task<User?> GetByIdAsync(int id);
    Task<User> CreateAsync(User userModel);
    Task WithdrawalAsync(int id, decimal amount);
    Task DepositAsync(int id, decimal amount);

    void TransactionPolicy(User payer, decimal value);
}
