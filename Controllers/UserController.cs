using Microsoft.AspNetCore.Mvc;
using TaskManagerAPI.Models;
using TaskManagerAPI.Interfaces;
using TaskManagerAPI.DTOs;

namespace TaskManagerAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetAll()
    {
        var users = await _userRepository.GetAllAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetById(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> Create(User user)
    {
        if (string.IsNullOrWhiteSpace(user.Username) || string.IsNullOrWhiteSpace(user.Password))
            return BadRequest("Usuário e senha são obrigatórios.");

        var existeUsuario = await _userRepository.AuthenticateAsync(user.Username, user.Password);

        if (existeUsuario != null)
        {
            return Conflict("Já existe um usuário com esse nome.");
        }

        await _userRepository.CreateAsync(user);
        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, User updateUser)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if (user == null)
            return NotFound();

        user.Username = updateUser.Username;
        user.Password = updateUser.Password;

        await _userRepository.UpdateAsync(user);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if (user == null)
            return NotFound();

        await _userRepository.DeleteAsync(id);
        return NoContent();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        if (loginRequest == null || string.IsNullOrEmpty(loginRequest.Username) || string.IsNullOrEmpty(loginRequest.Password))
        {
            return BadRequest("Usuário e senha são obrigatórios.");
        }

        var user = await _userRepository.AuthenticateAsync(loginRequest.Username, loginRequest.Password);

        if (user == null)
        {
            return Unauthorized("Usuário ou senha inválidos.");
        }

        return Ok(user);
    }
}



