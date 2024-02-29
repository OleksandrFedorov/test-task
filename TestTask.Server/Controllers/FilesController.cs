using Microsoft.AspNetCore.Mvc;
using TestTask.Dto.File;
using TestTask.Services.File;

namespace TestTask.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilesController(FileService fileService) : ControllerBase
    {
        private readonly FileService _fileService = fileService;

        /// <summary>
        /// Returns created file entity.
        /// </summary>
        /// <response code="200">Success creation.</response>
        /// <response code="400">Data is invalid.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPut("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AddAsync(Guid userId, [FromForm] IFormFile file, CancellationToken cancellationToken)
        {
            try
            {
                if (file is null)
                {
                    return BadRequest("File data must be specified");
                }

                if (string.IsNullOrWhiteSpace(file.Name))
                {
                    return BadRequest("File name must be specified");
                }

                var fileDto = await _fileService.AddAsync(userId, file, cancellationToken);
                return fileDto is not null
                    ? Ok(file)
                    : Problem("Internal server error while file creation");
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        /// <summary>
        /// Returns all files.
        /// </summary>
        /// <response code="200">Requested data.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            try
            {
                var files = await _fileService.GetAllAsync(cancellationToken);
                return Ok(files);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        /// <summary>
        /// Returns file.
        /// </summary>
        /// <response code="200">File entity.</response>
        /// <response code="404">File was not found.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var file = await _fileService.GetFileAsync(id, cancellationToken);
                if (file is null)
                {
                    return NotFound();
                }

                return Ok(file);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        /// <summary>
        /// Returns file stream.
        /// </summary>
        /// <response code="200">File stream.</response>
        /// <response code="404">File was not found.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet("download/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DownloadAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var file = await _fileService.GetFileAsync(id, cancellationToken);
                if (file is null)
                {
                    return NotFound();
                }

                var fileStream = _fileService.GetFileStream(file.Path);
                if (fileStream is null)
                {
                    return NotFound();
                }

                await _fileService.UpdateCountOnDownloadAsync(id, cancellationToken);
                return File(fileStream, "application/octet-stream", $"{file.Name}.{file.Path.Split('.').Last()}");
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        /// <summary>
        /// Returns result of file deleting.
        /// </summary>
        /// <response code="200">File deleted successfully.</response>
        /// <response code="500">Internal server error.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                await _fileService.DeleteAsync(id, cancellationToken);
                return Ok();
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        /// <summary>
        /// Returns created file share entity.
        /// </summary>
        /// <response code="200">Success creation.</response>
        /// <response code="200">File was not found.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPut("shares")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateFileSharesByFileIdAsync(FileShareCreateRequestDto dto, CancellationToken cancellationToken)
        {
            try
            {
                var share = await _fileService.ShareAsync(dto, cancellationToken);
                if (share is null)
                {
                    return NotFound();
                }

                return Ok(share);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        /// <summary>
        /// Returns a file by given share id.
        /// </summary>
        /// <response code="200">Requested file.</response>
        /// <response code="404">File share is not found.</response>
        /// <response code="419">File share is expiried.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet("shares/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFileByShareIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var share = await _fileService.GetFileByShareIdAsync(id, cancellationToken);
                if (share is null)
                {
                    return NotFound("File share is not found");
                }

                if (share is null)
                {
                    return Forbid("File share is expiried");
                }

                return Ok(share);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        /// <summary>
        /// Returns result of file share deleting.
        /// </summary>
        /// <response code="200">File share deleted successfully.</response>
        /// <response code="500">Internal server error.</response>
        [HttpDelete("shares/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteShareAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                await _fileService.DeleteShareAsync(id, cancellationToken);
                return Ok();
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
    }
}
