using MalDeals.Models.DTOs;
using MalDeals.Models.Entitys;
using MalDeals.Services;
using Microsoft.AspNetCore.Mvc;

namespace MalDeals.Controllers
{
    [ApiController]
    [Route("api/tags")]
    public class TagController(TagServices services, ILogger<TagController> logger) : ControllerBase
    {
        private readonly ILogger<TagController> _logger = logger;
        private readonly TagServices _services = services;
        [HttpGet]
        public async Task<IActionResult> GetTags()
        {
            try
            {
                IEnumerable<TagEntity> tags = await _services.GetTagsAsync();
                return Ok(tags);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTagById(Guid id)
        {
            try
            {
                TagEntity? tag = await _services.GetTagByIdAsync(id);
                if (tag == null)
                {
                    return NotFound();
                }
                return Ok(tag);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTag(TagDto tag)
        {
            try
            {
                TagEntity model = await _services.CreateTagAsync(tag.ToModel());
                return Created("", model);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(Guid id)
        {
            try
            {
                TagEntity? tag = await _services.GetTagByIdAsync(id);
                if (tag == null)
                {
                    return NotFound();
                }
                await _services.DeleteTagAsync(tag);
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