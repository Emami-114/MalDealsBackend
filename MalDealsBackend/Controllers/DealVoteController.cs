using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MalDeals.Models.Entitys;
using MalDeals.Services;
using Microsoft.AspNetCore.Mvc;

namespace MalDeals.Controllers
{
    [ApiController]
    [Route("api/vote")]
    public class DealVoteController(DealVoteServices services, ILogger<DealVoteController> logger) : ControllerBase
    {
        private readonly DealVoteServices _services = services;
        private readonly ILogger<DealVoteController> _logger = logger;
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDealVotes(Guid id)
        {
            try
            {
                IEnumerable<DealVoteEntity> dealVotes = await _services.GetDealVoteByDealIdAsync(id);
                return Ok(dealVotes);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("deal/{dealId}/user/{userId}")]
        public async Task<IActionResult> GetDealVoteByDealIdAndUserId(Guid dealId, Guid userId)
        {
            try
            {
                var dealVote = await _services.GetDealVoteByDealIdAndUserIdAsync(dealId, userId);
                if (dealVote == null)
                {
                    return NotFound();
                }
                return Ok(dealVote);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateDealVote(DealVoteEntity dealVote)
        {
            try
            {
                var IsDealVoteExists = await _services.GetDealVoteByDealIdAndUserIdAsync(dealVote.DealId, dealVote.UserId) != null;
                if (IsDealVoteExists)
                {
                    return Conflict("Deal Vote already exists.");
                }

                DealVoteEntity dealVoteEntity = await _services.CreateDealVoteAsync(dealVote);
                return Created("", dealVoteEntity);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpDelete("deal/{dealId}/user/{userId}")]
        public async Task<IActionResult> DeleteDealVote(Guid dealId, Guid userId)
        {
            try
            {
                DealVoteEntity? dealVote = await _services.GetDealVoteByDealIdAndUserIdAsync(dealId, userId);
                if (dealVote == null)
                {
                    return NotFound();
                }
                await _services.DeleteDealVoteByUserIdAsync(dealVote);
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