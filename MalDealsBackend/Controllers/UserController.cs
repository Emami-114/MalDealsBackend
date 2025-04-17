using MalDealsBackend.Models.DTOs;
using MalDealsBackend.Models.Entitys;
using MalDealsBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace MalDealsBackend.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController(UserServices services, ILogger<UserController> logger) : ControllerBase
    {
        private readonly UserServices _services = services;
        private readonly ILogger<UserController> _logger = logger;

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                IEnumerable<UserDataEntity> users = await _services.GetUsersDataAsync();
                return Ok(UserDataModelDto.ToDtos(users));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMe()
        {
            try
            {
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "userId");
                if (userIdClaim == null) return Unauthorized("User ID not found in token.");

                if (!Guid.TryParse(userIdClaim.Value, out Guid userId))
                    return BadRequest("Invalid user ID in token.");

                var user = await _services.GetUserDataByIdAsync(userId);
                if (user == null) return NotFound();

                return Ok(UserDataModelDto.ToDto(user));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            try
            {
                UserDataEntity? user = await _services.GetUserDataByIdAsync(id);
                if (user is null)
                {
                    return NotFound();
                }
                return Ok(UserDataModelDto.ToDto(user));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserData(Guid id, UserDataEntity userData)
        {
            try
            {
                UserDataEntity? user = await _services.GetUserDataByIdAsync(id);
                if (user is null)
                {
                    return NotFound();
                }

                await _services.UpdateUserDataAsync(user);
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