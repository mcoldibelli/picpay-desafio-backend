using api.Data;
using api.Exceptions;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _dbContext.User.AsNoTracking().ToListAsync();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _dbContext.User.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User> CreateAsync(User userModel)
    {
        if (await _dbContext.User
                .AnyAsync(u => u.Email == userModel.Email || u.Document == userModel.Document))
        {
            throw new InvalidOperationException("Email or Document already exists");
        }

        await _dbContext.User.AddAsync(userModel);
        await _dbContext.SaveChangesAsync();
        return userModel;
    }

    public async Task<User> UpdateAsync(User userModel)
    {
        var existingUser = await _dbContext.User.FindAsync(userModel.Id);

        if (existingUser == null)
        {
            throw new UserNotfoundException();
        }

        _dbContext.Entry(existingUser).CurrentValues.SetValues(userModel);
        await _dbContext.SaveChangesAsync();

        return existingUser;
    }
}
