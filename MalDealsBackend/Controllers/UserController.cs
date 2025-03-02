using MalDeals.Models.Entitys;
using MalDeals.Services;
using Microsoft.AspNetCore.Mvc;

namespace MalDeals.Controllers
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
                return Ok(users);
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
                return Ok(user);
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