using api.Models;

namespace api.Interfaces;

public interface ITransferRepository
{
    Task<Transfer> CreateAsync(Transfer transferModel);
}
