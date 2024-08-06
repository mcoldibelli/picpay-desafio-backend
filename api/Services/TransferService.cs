using api.Data;
using api.Exceptions;
using api.Interfaces;
using api.Models;

namespace api.Services;

public class TransferService : ITransferService
{
    private readonly ITransferRepository _transferRepository;
    private readonly IUserService _userService;
    private readonly ApplicationDbContext _dbContext;
    private readonly IAuthorizationService _authorizationService;

    public TransferService(
        ITransferRepository transferRepository,
        IUserService userService,
        ApplicationDbContext dbContext,
        IAuthorizationService authorizationService)
    {
        _transferRepository = transferRepository;
        _userService = userService;
        _dbContext = dbContext;
        _authorizationService = authorizationService;
    }

    // TODO Refactor, too big
    public async Task<Transfer> CreateAsync(Transfer transferModel)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync();

        try
        {
            var payer = await _userService.GetByIdAsync(transferModel.PayerId);
            var payee = await _userService.GetByIdAsync(transferModel.PayeeId);

            if (payer == null || payee == null)
            {
                throw new UserNotfoundException();
            }

            _userService.TransactionPolicy(payer, transferModel.Value);

            // TODO Rethrowing when false?
            var isAuthorized = await _authorizationService.GetAuthorizationAsync();
            if (!isAuthorized)
            {
                throw new UnauthorizedAccessException("API authorization failed.");
            }

            await _userService.WithdrawalAsync(payer.Id, transferModel.Value);
            await _userService.DepositAsync(payee.Id, transferModel.Value);

            // TODO API Notification (POST)
            // https://util.devi.tools/api/v1/notify)

            await _transferRepository.CreateAsync(transferModel);

            await transaction.CommitAsync();

            return transferModel;
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}
