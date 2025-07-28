using Microsoft.AspNetCore.Mvc;
using TaskManagerAPI.Models;
using TaskManagerAPI.Interfaces;
using TaskManagerAPI.DTOs;
using TaskManagerAPI.Services;
using Microsoft.AspNetCore.Authorization;

namespace TaskManagerAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly JwtTokenGenerator _jwtTokenGenerator;

    public UserController(IUserRepository userRepository, JwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    [AllowAnonymous] // Permitindo acesso sem autenticação apenas para testes
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetAll()
    {
        try
        {
            var users = await _userRepository.GetAllAsync();
            return Ok(users); // Retorna os usuários cadastrados incluindo a senha para facilitar o teste
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return StatusCode(500, "Ocorreu um erro ao buscar os usuários.");
        }
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetById(int id)
    {
        try
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            var userResponse = new
            {
                Id = user.Id,
                Username = user.Username,
            };
            
            return Ok(userResponse);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return StatusCode(500, "Ocorreu um erro ao buscar o usuário.");
        }
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Create(User user)
    {
        try
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
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return StatusCode(500, "Ocorreu um erro ao criar o usuário.");
        }
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, User updateUser)
    {
        try
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return NotFound("Usuário não encontrado.");
            
            var userComMesmoNome = await _userRepository.GetByUsernameAsync(updateUser.Username);
            
            if (userComMesmoNome != null && userComMesmoNome.Id != id)
                {
                    return Conflict("Já existe outro usuário com esse nome.");
                }

            user.Username = updateUser.Username;
            user.Password = updateUser.Password;

            await _userRepository.UpdateAsync(user);
            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return StatusCode(500, "Ocorreu um erro ao atualizar o usuário.");
        }
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                return NotFound("Usuário não encontrado.");

            await _userRepository.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return StatusCode(500, "Ocorreu um erro ao deletar o usuário.");
        }
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        try
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

            var token = _jwtTokenGenerator.GenerateToken(user);

            return Ok(new
            {
                token,
                user = new { user.Id, user.Username }
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return StatusCode(500, "Ocorreu um erro ao realizar o login.");
        }
    }
}