using MalDealsBackend.Models.DTOs;
using MalDealsBackend.Models.Entitys;
using MalDealsBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace MalDealsBackend.Controllers
{
    [ApiController]
    [Route("api/providers")]
    public class ProviderController(ProviderServices services, ILogger<ProviderController> logger) : ControllerBase
    {
        private readonly ILogger<ProviderController> _logger = logger;
        private readonly ProviderServices _services = services;

        [HttpGet]
        public async Task<IActionResult> GetProviders()
        {
            try
            {
                IEnumerable<ProviderEntity> providers = await _services.GetProvidersAsync();
                return Ok(providers);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProviderById(Guid id)
        {
            try
            {
                ProviderEntity? provider = await _services.GetProviderByIdAsync(id);
                if (provider == null)
                {
                    return NotFound();
                }
                return Ok(provider);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProvider(CreateProviderModelDto provider)
        {
            try
            {
                if (provider.Title.Length < 2)
                {
                    return BadRequest("Title must be at least 2 characters long.");
                }
                bool exists = await _services.ProviderExistsByTitleAsync(provider.Title);
                if (exists)
                {
                    return Conflict("A provider with the same title already exists.");
                }
                ProviderEntity model = await _services.CreateProviderAsync(provider.ToModel());
                return Created("", model);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProvider(Guid id, UpdateProviderModelDto updateProvider)
        {
            try
            {
                ProviderEntity? provider = await _services.GetProviderByIdAsync(id);
                if (provider == null)
                {
                    return NotFound();
                }
                await _services.UpdateProviderAsync(updateProvider.ToModel(provider));
                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProvider(Guid id)
        {
            try
            {
                ProviderEntity? provider = await _services.GetProviderByIdAsync(id);
                if (provider == null)
                {
                    return NotFound();
                }
                await _services.DeleteProviderAsync(provider);
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