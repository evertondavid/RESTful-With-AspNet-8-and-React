// Purpose: FileDetailVO class for the project.
namespace RestfullWithAspNet.Data.VO
{
    // Represents the details of a file.
    public class FileDetailVO
    {
        // Gets or sets the name of the document.
        public string? DocumentName { get; set; }

        // Gets or sets the type of the document.
        public string? DocType { get; set; }

        // Gets or sets the URL of the document.
        public string? DocUrl { get; set; }
    }
}
