using api.Interfaces;
using api.Models;

namespace api.Services;

public class TransferService : ITransferService
{
    private readonly ITransferRepository _transferRepository;

    public TransferService(ITransferRepository transferRepository)
    {
        _transferRepository = transferRepository;
    }

    public async Task<Transfer> CreateAsync(Transfer transferModel)
    {
        return await _transferRepository.CreateAsync(transferModel);
    }
}
