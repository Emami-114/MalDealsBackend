using MalDealsBackend.Models.Entitys;
using MalDealsBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace MalDealsBackend.Controllers
{
    [ApiController]
    [Route("api/marked-deals")]
    public class MarketDealController(MarketDealServices services, ILogger<MarketDealController> logger) : ControllerBase
    {
        private readonly MarketDealServices _services = services;
        private readonly ILogger<MarketDealController> _logger = logger;

        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetMarketDealByUserId(Guid id)
        {
            try
            {
                IEnumerable<UserMarketDealEntity> marketDeals = await _services.GetMarketDealByUserIdAsync(id);
                return Ok(marketDeals);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateMarketDeal(UserMarketDealEntity marketDeal)
        {
            try
            {
                UserMarketDealEntity userMarketDeal = await _services.CreateUserMarketAsync(marketDeal);
                return Created("", userMarketDeal);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpDelete("user/{userId}/deal/{dealId}")]
        public async Task<IActionResult> DeleteMarketDealByUser(Guid dealId, Guid userId)
        {
            try
            {
                UserMarketDealEntity? marketDealEntity = await _services.GetMarketDealByDealIdAndUserIdAsync(dealId, userId);
                if (marketDealEntity == null)
                {
                    return NotFound();
                }
                await _services.DeleteMarketDealByUserIdAsync(marketDealEntity);
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