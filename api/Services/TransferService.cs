using api.Exceptions;
using api.Interfaces;
using api.Models;

namespace api.Services;

public class TransferService : ITransferService
{
    private readonly ITransferRepository _transferRepository;
    private readonly IUserService _userService;

    public TransferService(ITransferRepository transferRepository, IUserService userService)
    {
        _transferRepository = transferRepository;
        _userService = userService;
    }

    public async Task<Transfer> CreateAsync(Transfer transferModel)
    {
        var payer = await _userService.GetByIdAsync(transferModel.PayerId);
        var payee = await _userService.GetByIdAsync(transferModel.PayeeId);

        if (payer == null || payee == null)
        {
            throw new UserNotfoundException();
        }

        _userService.TransactionPolicy(payer, transferModel.Value);

        // TODO API authentication

        // TODO rollback transaction
        await _userService.WithdrawalAsync(payer.Id, transferModel.Value);
        await _userService.DepositAsync(payee.Id, transferModel.Value);

        // TODO API Notification
        return await _transferRepository.CreateAsync(transferModel);
    }
}
