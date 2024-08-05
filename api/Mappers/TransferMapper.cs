using api.Dtos.Transfer;
using api.Models;

namespace api.Mappers;

public static class TransferMapper
{
    public static Transfer ToTransferFromCreateDto(this CreateTransferRequestDto transferModel)
    {
        return new Transfer
        {
            Value = transferModel.Value,
            PayerId = transferModel.PayerId,
            PayeeId = transferModel.PayeeId,
        };
    }
}
