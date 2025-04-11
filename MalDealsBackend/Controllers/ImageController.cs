using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MalDealsBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace MalDealsBackend.Controllers
{
    [ApiController]
    [Route("api/images")]
    public class ImageController : ControllerBase
    {
        private readonly MinioService _minioService;
        private readonly ILogger<ImageController> _logger;

        public ImageController(MinioService minioService, ILogger<ImageController> logger)
        {
            _minioService = minioService;
            _logger = logger;
        }

        [HttpPost("{bucketName}")]
        public async Task<IActionResult> UploadFile(string bucketName, IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest();
                }

                await _minioService.UploadFileAsync(bucketName, file);
                var fileName = file.FileName.Replace(" ","-") ;
                var fileNameWithBucket = $"{bucketName}/{fileName}";
                return Created("", fileNameWithBucket);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost("multiple/{bucketName}")]
        public async Task<IActionResult> UploadMultipleImage(string bucketName, List<IFormFile> files)
        {
            try
            {
                if (files == null || files.Count == 0)
                {
                    return BadRequest();
                }

                var fileUrls = await _minioService.UploadMultipleFileAsync(bucketName, files);
                return Created("", fileUrls);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("{bucketName}/{filePath}")]
        public async Task<IActionResult> GetFileUrlByFileName(string bucketName,string filePath)
        {
            try
            {
                var split = filePath.Split("/");
                var fileUrl = await _minioService.GetPresignedUrlAsync(split[0], split[1]);
                return Ok(fileUrl);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpDelete("{bucketName}/{filePath}")]
        public async Task<IActionResult> DeleteFile(string bucketName,string filePath)
        {
            try
            {
                var split = filePath.Split("/");
                bool success = await _minioService.DeleteFileAsync(split[0], split[1]);
                if (success)
                {
                    return NoContent();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

    }
}