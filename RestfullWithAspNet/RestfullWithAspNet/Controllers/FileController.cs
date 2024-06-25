using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestfullWithAspNet.Business;
using RestfullWithAspNet.Data.VO;

namespace RestfullWithAspNet.Controllers
{
    /// <summary>
    /// Controller responsible for file operations
    /// </summary>
    [ApiVersion("1")]
    [ApiController]
    [Authorize("Bearer")]
    [Route("api/[controller]/v{version:ApiVersion}")]
    public class FileController : ControllerBase
    {
        private readonly IFileBusiness _fileBusiness;

        /// <summary>
        /// Constructor for the FileController
        /// </summary>
        /// <param name="fileBusiness"></param>
        public FileController(IFileBusiness fileBusiness)
        {
            _fileBusiness = fileBusiness;
        }

        /// <summary>
        /// Save a file to disk
        /// </summary>
        /// <param name="file">File to be saved</param>
        /// <returns>File details</returns>
        [HttpPost("uploadFile")]
        [ProducesResponseType((200), Type = typeof(FileDetailVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Produces("application/json", "application/xml")]
        public async Task<IActionResult> UploadOneFile([FromForm] IFormFile file)
        {
            FileDetailVO detail = await _fileBusiness.SaveFileToDisk(file);
            return new OkObjectResult(detail);
        }


        /// <summary>
        ///  Save multiple files to disk and return the file details
        /// </summary>
        /// <param name="file">File to be saved</param>
        /// <returns>File details</returns>
        [HttpPost("uploadFileToDatabase")]
        [ProducesResponseType((200), Type = typeof(FileDetailVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Produces("application/json", "application/xml")]
        public async Task<IActionResult> UploadOneFileToDatabase([FromForm] IFormFile file)
        {
            FileDetailVO detail = await _fileBusiness.SaveFileToDatabase(file);
            return new OkObjectResult(detail);
        }

        /// <summary>
        /// Save multiple files to disk and return the file details
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        [HttpPost("uploadMultipleFiles")]
        [ProducesResponseType((200), Type = typeof(List<FileDetailVO>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Produces("application/json", "application/xml")]
        public async Task<IActionResult> UploadManyFiles([FromForm] List<IFormFile> files)
        {
            List<FileDetailVO> details = await _fileBusiness.SaveFilesToDisk(files);
            return new OkObjectResult(details);
        }

        /// <summary>
        /// Download a file from the disk
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [HttpGet("downloadFile/{fileName}")]
        [ProducesResponseType((200), Type = typeof(byte[]))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Produces("application/octet-stream")]
        public async Task<IActionResult> GetFileAsync(string fileName)
        {
            byte[] buffer = _fileBusiness.GetFile(fileName); // Get the file from the business layer
            if (buffer != null) // If the buffer is not null then set the content type and length of the response
            {
                HttpContext.Response.ContentType =
                $"application/{Path.GetExtension(fileName).Replace(".", "")}"; // Set the content type of the response

                HttpContext.Response.Headers.Add("content-length", buffer.Length.ToString()); // Set the content length of the response

                await HttpContext.Response.Body.WriteAsync(buffer, 0, buffer.Length); // Write the buffer to the response body and return the response
            }
            return new ContentResult();
        }
    }
}
