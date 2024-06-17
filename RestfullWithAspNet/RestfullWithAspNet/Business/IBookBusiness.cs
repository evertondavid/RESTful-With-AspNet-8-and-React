using RestfullWithAspNet.Data.VO;
using System.Collections.Generic; // Added using statement for List<T>

namespace RestfullWithAspNet.Business
{
    /// <summary>
    /// Interface for the Business service.
    /// </summary>
    public interface IBookBusiness
    {
        /// <summary>
        /// Creates a new book.
        /// </summary>
        /// <param name="book">The book to be created.</param>
        /// <returns>The book created.</returns>
        BookVO Create(BookVO book);

        /// <summary>
        /// Find a book by its ID.
        /// </summary>
        /// <param name="id">The ID of the book.</param>
        /// <returns>The book with the specific ID.</returns>
        BookVO FindById(long id);

        /// <summary>
        /// Find all books.
        /// </summary>
        /// <returns>List of books.</returns>
        List<BookVO> FindAll(); // Updated comment to reflect that it returns a list of books

        /// <summary>
        /// Updates a book.
        /// </summary>
        /// <param name="book">The book to be updated.</param>
        /// <returns>The book updated.</returns>
        BookVO Update(BookVO book);

        /// <summary>
        /// Delete a book by its ID.
        /// </summary>
        /// <param name="id">The ID of the book to be deleted.</param>
        void Delete(long id);
    }
}
