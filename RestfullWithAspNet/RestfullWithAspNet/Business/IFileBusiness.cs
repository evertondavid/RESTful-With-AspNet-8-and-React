using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestfullWithAspNet.Data.VO;

namespace RestfullWithAspNet.Business
{
    // This interface defines the contract for file-related operations
    public interface IFileBusiness
    {
        // Retrieves a file from the disk based on the provided filename
        public byte[] GetFile(string filename);

        // Saves a single file to the disk and returns the file details
        public Task<FileDetailVO> SaveFileToDisk(IFormFile file);

        // Saves multiple files to the disk and returns the file details for each file
        public Task<List<FileDetailVO>> SaveFilesToDisk(IList<IFormFile> files);
    }
}
