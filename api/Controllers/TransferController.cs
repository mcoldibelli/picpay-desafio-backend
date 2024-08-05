using api.Dtos.Transfer;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Route("transfer")]
[ApiController]
public class TransferController : ControllerBase
{
    private readonly ITransferService _transferService;

    public TransferController(ITransferService transferService)
    {
        _transferService = transferService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTransferRequestDto transferDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var transferModel = transferDto.ToTransferFromCreateDto();
        await _transferService.CreateAsync(transferModel);
        return NoContent();
    }
}
