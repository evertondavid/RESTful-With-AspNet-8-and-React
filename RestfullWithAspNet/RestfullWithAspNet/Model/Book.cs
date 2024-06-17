using System.ComponentModel.DataAnnotations.Schema;
using RestfullWithAspNet.Model.Base;

namespace RestfullWithAspNet.Model
{
    [Table("book")]
    /// <summary>
    /// Classe Book that represents a book.
    /// </summary>
    public class Book : BaseEntity
    {
        /// <summary>
        /// Get or set the book's author.
        /// </summary>
        private string _author = string.Empty;
        [Column("author")]
        public string Author
        {
            get => _author;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Author name cannot be null or empty.");
                }
                _author = value;
            }
        }
        /// <summary>
        /// Get or set the book's title.
        /// </summary>
        private string _title = string.Empty;
        [Column("title")]
        public string Title
        {
            get => _title;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Title cannot be null or empty.");
                }
                _title = value;
            }
        }
        /// <summary>
        /// Gets or sets the address. The address should not be null or empty.
        /// </summary>
        private DateTime _launchDate = DateTime.Now;
        [Column("launch_date")]
        public DateTime LaunchDate
        {
            get => _launchDate;
            set => _launchDate = value;
        }

        /// <summary>
        /// Get or set the Price.
        /// </summary>
        private decimal _price = 0;
        [Column("price")]
        public decimal Price
        {
            get => _price;
            set
            {
                _price = value;
            }
        }
    }
}
