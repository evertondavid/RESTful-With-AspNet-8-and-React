// This class is responsible for representing the book object.
// The Book class is a Value Object (VO) that represents the book object.
// The Book class has the following properties:
// Id: represents the book's ID.
// Author: represents the book's author.
// Title: represents the book's title.
// LaunchDate: represents the book's launch date.
// Price: represents the book's price.


using HATEOAS.Hypermedia.Abstract;
using HATEOAS.Hypermedia.Filters;

namespace RestfullWithAspNet.Data.VO
{
    /// <summary>
    /// Classe Book that represents a book.
    /// </summary>
    public class BookVO : ISupportsHyperMedia
    {
        /// <summary>
        /// Get or set the person's ID.
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Get or set the book's author.
        /// </summary>
        private string _author = string.Empty;
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
        public DateTime LaunchDate
        {
            get => _launchDate;
            set => _launchDate = value;
        }

        /// <summary>
        /// Get or set the Price.
        /// </summary>
        private decimal _price = 0;
        public decimal Price
        {
            get => _price;
            set
            {
                _price = value;
            }
        }

        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }
}
