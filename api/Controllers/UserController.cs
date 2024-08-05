using api.Dtos.User;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Route("api/user")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await  _userService.GetAllAsync();
        var userDto = users.Select(u => u.ToUserDto());
        return Ok(userDto);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var user = await _userService.GetByIdAsync(id);
        if (user == null)
            return NotFound();
        return Ok(user.ToUserDto());
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserRequestDto userDTO)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userModel = userDTO.ToUserFromCreateDto();
        var createdUser = await _userService.CreateAsync(userModel);
        return CreatedAtAction(nameof(GetById), new { id = createdUser.Id }, createdUser.ToUserDto());
    }

}
