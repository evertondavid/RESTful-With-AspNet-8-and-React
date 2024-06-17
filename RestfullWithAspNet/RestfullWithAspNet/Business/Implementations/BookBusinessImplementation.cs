using RestfullWithAspNet.Data.Converter.Implementations;
using RestfullWithAspNet.Data.VO;
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
        private readonly BookConverter _converter;

        /// <summary>
        /// Initializes a new instance of the BookServiceImplementation class.
        /// </summary>
        /// <param name="context">Database context for accessing book.</param>
        public BookBusinessImplementation(IRepository<Book> repository)
        {
            _repository = repository;
            _converter = new BookConverter();
        }

        /// <summary>
        /// Retrieves all books from the database.
        /// </summary>
        /// <returns>A list of all books.</returns>
        public List<BookVO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        }

        /// <summary>
        /// Finds a book by its ID.
        /// </summary>
        /// <param name="id">The ID of the book to find.</param>
        /// <returns>The book with the specified ID, or throws a KeyNotFoundException if not found.</returns>
        public BookVO FindById(long id)
        {
            var book = _repository.FindById(id);
            return _converter.Parse(book) ?? throw new KeyNotFoundException($"The book with ID: {id} was not found.");
        }

        /// <summary>
        /// Creates a new book in the database.
        /// </summary>
        /// <param name="book">The book to create.</param>
        /// <returns>The created book.</returns>
        public BookVO Create(BookVO book)
        {
            var bookEntity = _converter.Parse(book); // Convert the VO to an Entity
            bookEntity = _repository.Create(bookEntity); // Create the entity in the database
            return _converter.Parse(bookEntity); // Convert the entity back to a VO
        }

        /// <summary>
        /// Updates an existing book in the database.
        /// </summary>
        /// <param name="book">The book to update.</param>
        /// <returns>The updated book, or a new Book if the original does not exist.</returns>
        public BookVO Update(BookVO book)
        {
            var bookEntity = _converter.Parse(book); // Convert the VO to an Entity
            bookEntity = _repository.Update(bookEntity); // Update the entity in the database
            return _converter.Parse(bookEntity); // Convert the entity back to a VO
        }

        /// <summary>
        /// Deletes a book from the database by its ID.
        /// </summary>
        /// <param name="id">The ID of the book to delete.</param>
        public void Delete(long id)
        {
            _repository.Delete(id);
        }
    }
}
