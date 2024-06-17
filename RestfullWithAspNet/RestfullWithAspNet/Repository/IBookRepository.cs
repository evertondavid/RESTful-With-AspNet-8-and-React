using RestfullWithAspNet.Model;

namespace RestfullWithAspNet.Repository
{
    /// <summary>
    /// Interface for the  service.
    /// </summary>
    public interface IBookRepository
    {
        /// <summary>
        /// Creates a new book.
        /// </summary>
        /// <param name="book">The book to be created.</param>
        /// <returns>The book created.</returns>
        Book Create(Book book);

        /// <summary>
        /// Find a book by its ID.
        /// </summary>
        /// <param name="id">The ID of the book.</param>
        /// <returns>The book with the specific ID.</returns>
        Book FindById(long id);

        /// <summary>
        /// Find all people.
        /// </summary>
        /// <returns>List of people.</returns>
        List<Book> FindAll();

        /// <summary>
        /// Updates a book.
        /// </summary>
        /// <param name="book">The book to be updated.</param>
        /// <returns>The book updated.</returns>
        Book Update(Book book);

        /// <summary>
        /// Delete a book by its ID.
        /// </summary>
        /// <param name="id">The ID of the book to be deleted.</param>
        void Delete(long id);

        /// <summary>
        /// Checks if a book exists in the database by their ID.
        /// </summary>
        /// <param name="id">The ID of the book to check.</param>
        /// <returns>True if the book exists, false otherwise.</returns>
        bool Exists(long id);
    }
}
