using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MalDealsBackend.Models;
using MalDealsBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MalDealsBackend.Controllers
{
    [ApiController]
    [Route("api/request-api-key")]
    public class ApiKeyController(ApiKeyService service, ILogger<ApiKeyController> logger) : ControllerBase
    {
        private readonly ApiKeyService _service = service;
        private readonly ILogger<ApiKeyController> _logger = logger;

        [AllowAnonymous]
        [HttpPost]
        public IActionResult RequestApiKey([FromBody] ApiKeyRequestModel apiKeyModel)
        {
            try
            {
                if (!_service.ValidateSignature(apiKeyModel.DeviceId, apiKeyModel.Timestamp, apiKeyModel.Signature))
                {
                    return Unauthorized();
                }

                string apiKey = _service.ApiKeyGenerator(apiKeyModel.DeviceId);
                return Ok(apiKey);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

    }
}