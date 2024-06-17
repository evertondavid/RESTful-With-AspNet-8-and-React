using RestfullWithAspNet.Model;
using RestfullWithAspNet.Repository;

namespace RestfullWithAspNet.Business.Implementations
{
    /// <summary>
    /// Provides implementation for the IBookBusiness interface using Rules.
    /// </summary>
    public class BookBusinessImplementation : IBookBusiness
    {
        private readonly IRepository<Book> _repository;

        /// <summary>
        /// Initializes a new instance of the BookServiceImplementation class.
        /// </summary>
        /// <param name="context">Database context for accessing book.</param>
        public BookBusinessImplementation(IRepository<Book> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Retrieves all book from the database.
        /// </summary>
        /// <returns>A list of all books.</returns>
        public List<Book> FindAll()
        {
            return _repository.FindAll();
        }

        /// <summary>
        /// Finds a book by their ID.
        /// </summary>
        /// <param name="id">The ID of the book to find.</param>
        /// <returns>The book with the specified ID, or throws a KeyNotFoundException if not found.</returns>
        public Book FindById(long id)
        {
            var book = _repository.FindById(id);
            return book ?? throw new KeyNotFoundException($"The book with ID: {id} was not found.");
        }

        /// <summary>
        /// Creates a new book in the database.
        /// </summary>
        /// <param name="book">The book to create.</param>
        /// <returns>The created book.</returns>
        public Book Create(Book book)
        {
            return _repository.Create(book);
        }

        /// <summary>
        /// Updates an existing book in the database.
        /// </summary>
        /// <param name="book">The book to update.</param>
        /// <returns>The updated book, or a new Book if the original does not exist.</returns>
        public Book Update(Book book)
        {
            return _repository.Update(book);
        }

        /// <summary>
        /// Deletes a book from the database by their ID.
        /// </summary>
        /// <param name="id">The ID of the book to delete.</param>
        public void Delete(long id)
        {
            _repository.Delete(id);
        }
    }
}
