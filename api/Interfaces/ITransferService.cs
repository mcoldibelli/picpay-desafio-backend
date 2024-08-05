using api.Models;

namespace api.Interfaces;

public interface ITransferService
{

    Task<Transfer> CreateAsync(Transfer transferModel);
}
