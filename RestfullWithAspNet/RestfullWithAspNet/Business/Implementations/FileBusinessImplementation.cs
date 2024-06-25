using RestfullWithAspNet.Data.VO;
using RestfullWithAspNet.Model.Context;

namespace RestfullWithAspNet.Business.Implementations
{
    public class FileBusinessImplementation : IFileBusiness
    {
        // The base path for the file storage
        private readonly string _basePath;
        // The HTTP context accessor for the current request
        private readonly IHttpContextAccessor _context;

        // The MySQL database context for the application
        private readonly MySQLContext _dbContext;

        /// <summary>
        /// Constructor for the FileBusinessImplementation class
        /// </summary>
        /// <param name="context"></param>
        public FileBusinessImplementation(IHttpContextAccessor context, MySQLContext dbContext)
        {
            _dbContext = dbContext;
            _context = context;
            _basePath = Path.Combine(Directory.GetCurrentDirectory(), "UploadDir" + Path.DirectorySeparatorChar); // Get the base path for the file storage
            if (!Directory.Exists(_basePath))
            {
                Directory.CreateDirectory(_basePath); // Create the directory if it does not exist
            }
        }

        /// <summary>
        /// Saves a file to the disk and returns the file details
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<FileDetailVO> SaveFileToDisk(IFormFile file)
        {
            FileDetailVO fileDetail = new FileDetailVO(); // FileDetailVO is a class in the project
            var fileType = Path.GetExtension(file.FileName); // .jpg

            if (_context.HttpContext == null || !_context.HttpContext.Request.Host.HasValue)
            {
                throw new Exception("Host not found");
            }

            var baseUrl = $"{_context.HttpContext.Request.Scheme}://{_context.HttpContext.Request.Host.Value}"; // http://localhost:5000
            if (fileType.ToLower() == ".pdf" || fileType.ToLower() == ".jpg" ||
                fileType.ToLower() == ".jpeg" || fileType.ToLower() == ".png") // Check if file type is valid
            {
                var docName = Path.GetFileName(file.FileName).Replace(" ", "-"); // Get the file name and remove spaces
                if (file != null && file.Length > 0)
                {
                    var destination = Path.Combine(_basePath, "", docName); // Create the path to save the file
                    fileDetail.DocumentName = docName; // Set the file name
                    fileDetail.DocType = fileType; // Set the file type
                    fileDetail.DocUrl = Path.Combine(baseUrl + "/api/file/v1/" + fileDetail.DocumentName); // Set the file URL to be accessed later
                    using (var stream = new FileStream(destination, FileMode.Create)) // Create the file in the path
                        await file.CopyToAsync(stream); // Copy the file to the stream
                }
            }
            return fileDetail;
        }

        /// <summary>
        /// Saves a file to the database and returns the file details
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<FileDetailVO> SaveFileToDatabase(IFormFile file)
        {
            FileDetailVO fileDetail = new FileDetailVO(); // FileDetailVO is a class in the project
            var fileType = Path.GetExtension(file.FileName); // .jpg

            if (_context.HttpContext == null || !_context.HttpContext.Request.Host.HasValue)
            {
                throw new Exception("Host not found");
            }

            var baseUrl = $"{_context.HttpContext.Request.Scheme}://{_context.HttpContext.Request.Host.Value}"; // http://localhost:5000

            if (fileType.ToLower() == ".pdf" || fileType.ToLower() == ".jpg" ||
                fileType.ToLower() == ".jpeg" || fileType.ToLower() == ".png") // Check if file type is valid
            {
                var docName = Path.GetFileName(file.FileName); // Get the file name
                if (file != null && file.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await file.CopyToAsync(memoryStream); // Copy the file to the memory stream
                        var fileBytes = memoryStream.ToArray(); // Convert the file to a byte array

                        //fileDetail.Id = (int?)Guid.NewGuid().GetHashCode(); // Set the file ID
                        fileDetail.DocumentName = docName; // Set the file name
                        fileDetail.DocType = fileType; // Set the file type
                        fileDetail.DocUrl = Path.Combine(baseUrl + "/api/file/v1/" + fileDetail.DocumentName); // Set the file URL to be accessed later
                        fileDetail.DocData = fileBytes; // Set the file data

                        // Save file details to the database using the injected context
                        _dbContext.FileDetails.Add(fileDetail);
                        await _dbContext.SaveChangesAsync();
                    }
                }
            }
            return fileDetail;
        }

        /// <summary>
        /// Retrieves a file from the disk based on the provided filename
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public byte[] GetFile(string filename)
        {
            var filePath = _basePath + filename; // Get the file path based on the filename
            return File.ReadAllBytes(filePath); // Read the file bytes and return them
        }

        /// <summary>
        /// Saves multiple files to the disk and returns the file details for each file
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        public async Task<List<FileDetailVO>> SaveFilesToDisk(IList<IFormFile> files)
        {
            List<FileDetailVO> list = new List<FileDetailVO>(); // Create a list to store the file details
            foreach (var file in files)
            {
                list.Add(await SaveFileToDisk(file)); // Save each file to the disk
            }
            return list;
        }
    }
}
