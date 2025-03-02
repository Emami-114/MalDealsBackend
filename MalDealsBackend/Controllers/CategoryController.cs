using MalDealsBackend.Models.DTOs;
using MalDealsBackend.Models.Entitys;
using MalDealsBackend.Services;
using MalDealsBackend.AspNetCore.Mvc;

namespace MalDealsBackend.Controllers
{
    [ApiController]
    [Route("api/catgories")]
    public class CategoryController(CategoryServices service, ILogger<CategoryController> logger) : ControllerBase
    {
        private readonly CategoryServices _service = service;
        private readonly ILogger<CategoryController> _logger = logger;

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                IEnumerable<CategoryEntity> categories = await _service.GetCategoriesAsync();
                return Ok(categories);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            try
            {
                CategoryEntity? category = await _service.GetCategoryByIdAsync(id);
                if (category is null)
                {
                    return NotFound();
                }
                return Ok(category);
            }
            catch (System.Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryModelDto categoryModelDto)
        {
            try
            {
                if (categoryModelDto.Title.Length < 3)
                {
                    return BadRequest("Title must be at least 3 characters long.");
                }

                bool exists = await _service.CategoryExistsByTitleAsync(categoryModelDto.Title);
                if (exists)
                {
                    return Conflict("A category with the same title already exists.");
                }

                CategoryEntity category = await _service.CreateCategoryAsync(categoryModelDto.ToCategoryEntity());
                return Created("", category);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(Guid id, UpdateCategoryModelDto categoryModelDto)
        {
            try
            {
                CategoryEntity? categoryEntity = await _service.GetCategoryByIdAsync(id);
                if (categoryEntity is null)
                {
                    return NotFound();
                }

                await _service.UpdateCategoryAsync(categoryModelDto.ToCategoryEntity(categoryEntity));
                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            try
            {
                CategoryEntity? category = await _service.GetCategoryByIdAsync(id);
                if (category is null)
                {
                    return NotFound();
                }

                await _service.DeleteCategoryAsync(category);
                return NoContent();
            }
            catch (System.Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }

    }
}