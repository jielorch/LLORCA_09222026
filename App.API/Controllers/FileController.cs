using App.API.Models.Requests;
using App.Application.Common.Interfaces;
using App.Application.Features.FileRecords.DTO.Requests;
using App.Application.Features.FileRecords.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController(IFileRecordWriteService fileRecordWriteService,
                                IFileRecordReadService fileRecordReadService,
                                ILogger<FileController> _logger) : ControllerBase
    {
        [HttpGet("record")]
        public async Task<IActionResult> Record()
        {
            try
            {
                _logger.LogInformation("Successfully fetched the file record");

                var result = await fileRecordReadService.GetFileRecordsAsync();
                return Ok(result);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An unhandled error occurred while processing the record");

                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An internal server error occurred while analyzing the file." });
            }
           
        }

        [HttpPost("upload")]
        [Consumes("multipart/form-data")] // Explicitly tells Swagger UI to expect a file upload form
        public async Task<IActionResult> Upload([FromForm] FileUploadRequest fileUploadRequest, [FromServices] ICsvProcessor csvProcessor)
        {
            try
            {
                if (fileUploadRequest.File == null || fileUploadRequest.File.Length == 0)
                {
                    return BadRequest(new { Message = "No file was uploaded." });
                }

                var fileExtension = Path.GetExtension(fileUploadRequest.File.FileName).ToLowerInvariant();

                // Verify it explicitly matches ".csv"
                if (fileExtension != ".csv")
                {
                    return BadRequest(new { Message = "Invalid file format. Only .csv files are allowed." });
                }

                using var stream = fileUploadRequest.File.OpenReadStream();

                decimal calculatedAverageProfit = await csvProcessor.CalculateAverageFromCsvAsync(stream, "Profit");

                var fileRequest = new FileRequest
                {
                    FileName = fileUploadRequest.File.FileName,
                    Average = calculatedAverageProfit
                };

                var result = await fileRecordWriteService.UploadAsync(fileRequest);

                _logger.LogInformation("Successfully processed CSV file {FileName}",
                    fileUploadRequest.File.FileName);

                if (result)
                {
                    return Ok(new { Message = "File processed successfully." });
                }

                return BadRequest(new { Message = "Failed to process file" });

            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An unhandled error occurred while processing the uploaded file {FileName}.",
                    fileUploadRequest.File.FileName);

                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An internal server error occurred while analyzing the file." });
            }
        }
    }
}
