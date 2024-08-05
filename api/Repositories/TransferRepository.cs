using api.Data;
using api.Interfaces;
using api.Models;

namespace api.Repositories;

public class TransferRepository : ITransferRepository
{
    private readonly ApplicationDbContext _dbContext;
    public TransferRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Transfer> CreateAsync(Transfer transferModel)
    {
        await _dbContext.Transfer.AddAsync(transferModel);
        await _dbContext.SaveChangesAsync();
        return transferModel;
    }
}
