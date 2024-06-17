using RestfullWithAspNet.Data.Converter.Contract;
using RestfullWithAspNet.Data.VO;
using RestfullWithAspNet.Model;
using System.Collections.Generic;
using System.Linq;

namespace RestfullWithAspNet.Data.Converter.Implementations
{
    /// <summary>
    /// Converts between Book and BookVO objects.
    /// </summary>
    public class BookConverter : IParser<BookVO, Book>, IParser<Book, BookVO>
    {
        /// <summary>
        /// Converts a BookVO object to a Book object.
        /// </summary>
        /// <param name="origin">The BookVO object to convert.</param>
        /// <returns>The converted Book object.</returns>
        public Book Parse(BookVO origin)
        {
            if (origin == null) return null;
            return new Book
            {
                Id = origin.Id,
                Author = origin.Author,
                Title = origin.Title,
                LaunchDate = origin.LaunchDate,
                Price = origin.Price
            };
        }

        /// <summary>
        /// Converts a Book object to a BookVO object.
        /// </summary>
        /// <param name="origin">The Book object to convert.</param>
        /// <returns>The converted BookVO object.</returns>
        public BookVO Parse(Book origin)
        {
            if (origin == null) return null;
            return new BookVO
            {
                Id = origin.Id,
                Author = origin.Author,
                Title = origin.Title,
                LaunchDate = origin.LaunchDate,
                Price = origin.Price
            };
        }

        /// <summary>
        /// Converts a list of BookVO objects to a list of Book objects.
        /// </summary>
        /// <param name="origin">The list of BookVO objects to convert.</param>
        /// <returns>The converted list of Book objects.</returns>
        public List<Book> Parse(List<BookVO> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }

        /// <summary>
        /// Converts a list of Book objects to a list of BookVO objects.
        /// </summary>
        /// <param name="origin">The list of Book objects to convert.</param>
        /// <returns>The converted list of BookVO objects.</returns>
        public List<BookVO> Parse(List<Book> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }
    }
}
