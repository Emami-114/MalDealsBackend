using MalDealsBackend.Models.DTOs;
using MalDealsBackend.Models.Entitys;
using MalDealsBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace MalDealsBackend.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController(UserServices service, JwtService jwtService, PasswordService passwordService, ILogger<AuthController> logger) : ControllerBase
    {
        private readonly UserServices _services = service;
        private readonly JwtService _jwtService = jwtService;
        private readonly PasswordService _passwordServices = passwordService;
        private readonly ILogger<AuthController> _logger = logger;

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserModelDto userDto)
        {
            try
            {
                if (await _services.IsExistsUserByEmailAsync(userDto.Email))
                {
                    return Conflict("Username already exists.");
                }

                var user = new UserEntity
                {
                    Name = userDto.Name,
                    Email = userDto.Email,
                    Password = "",
                };
                var userData = new UserDataEntity
                {
                    UserId = user.Id,
                    Email = user.Email,
                    Name = user.Name,
                    Role = user.Role,
                    Verified = user.Verified
                };
                user.Password = _passwordServices.HashPassword(user, userDto.Password);

                UserEntity userEntity = await _services.CreateUserAsync(user);
                await _services.CreateUserDataAsync(userData);
                return Ok("Registration successful.");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserModelDto userDto)
        {
            try
            {
                UserEntity? user = await _services.GetUserByEmailAsync(userDto.Email);
                if (user is null || !_passwordServices.VerifyPassword(user, user.Password, userDto.Password))
                {
                    return NotFound();
                }

                var token = _jwtService.GenerateToken(user.Name, user.Role);
                return Ok(token);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserVerified(Guid id, UpdateUserVerifidDto userDto)
        {
            try
            {
                UserEntity? userEntity = await _services.GetUserByIdAsync(id);

                if (userEntity is null)
                {
                    return NotFound();
                }

                await _services.UpdateUserAsync(userDto.ToModel(userEntity));
                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

    }
}