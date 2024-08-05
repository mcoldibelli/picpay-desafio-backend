using api.Dtos.User;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Route("api/user")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await  _userRepository.GetAllAsync();

        var userDto = users.Select(u => u.ToUserDto());

        return Ok(userDto);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if (user == null)
            return NotFound();

        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserRequestDto userDTO)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userModel = userDTO.ToUserFromCreateDto();
        await _userRepository.CreateAsync(userModel);
        return CreatedAtAction(nameof(GetById), new { id = userModel.Id }, userModel.ToUserDto());
    }

}
