using MalDealsBackend.Models;
using MalDealsBackend.Models.DTOs;
using MalDealsBackend.Models.Entitys;
using MalDealsBackend.Services;
using MalDealsBackend.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MalDealsBackend.Controllers
{
    [ApiController]
    [Route("api/deals")]
    public class DealController(DealServices dealServices,DealVoteServices dealVoteServices ,ILogger<DealController> logger) : ControllerBase
    {
        private readonly ILogger<DealController> _logger = logger;
        private readonly DealServices _dealServices = dealServices;
        private readonly DealVoteServices _dealVoteService = dealVoteServices;

        [HttpGet]
        public async Task<ActionResult> GetDeals([FromQuery] DealFilterQuery query)
        {
            try
            {
                IEnumerable<DealEntity> deals = await _dealServices.GetDealsAsync(query);
                var dealsDto = new List<DealModelDto>(); 
                
                foreach (var deal in deals) {
                    var votes = await _dealVoteService.GetDealVoteByDealIdAsync(deal.Id);
                    if(votes != null) { deal.VoteCount  += votes.Count(); }
                    var dto = DealModelDto.ToDto(deal);
                    dealsDto.Add(dto);
                }
                return Ok(dealsDto);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetDealByIdAsync(Guid id)
        {
            try
            {
                DealEntity? dealModel = await _dealServices.GetDealByIdAsync(id);
                if (dealModel == null)
                {
                    return NotFound();
                }
                var dto = DealDetailModelDto.ToDto(dealModel);
                return Ok(dto);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateDealAsync(CreateDealDto dealDto)
        {
            try
            {
                var IsDealExists = await _dealServices.DealExistsByTitleAsync(dealDto.Title);
                if (IsDealExists)
                {
                    return Conflict();
                }

                DealEntity model = await _dealServices.CreateDealAsync(dealDto.ToModel());
                return Created("", model);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDealAsync(Guid id, UpdateDealModelDto dealDto)
        {
            try
            {
                DealEntity? dealModel = await _dealServices.GetDealByIdAsync(id);
                if (dealModel == null)
                {
                    return NotFound();
                }
                await _dealServices.UpdateDealAsync(dealDto.ToModel(dealModel));
                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDealAsync(Guid id)
        {
            try
            {
                DealEntity? deal = await _dealServices.GetDealByIdAsync(id);
                if (deal == null)
                {
                    return NotFound();
                }
                await _dealServices.DeleteDealAsync(deal);
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