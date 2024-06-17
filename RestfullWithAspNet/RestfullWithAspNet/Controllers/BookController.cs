using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using RestfullWithAspNet.Business;
using RestfullWithAspNet.Data.VO;

namespace RestfullWithAspNet.Controllers
{
    /// <summary>
    /// Controller for manipulating book data.
    /// </summary>
    [ApiVersion("1")]
    [ApiController]
    [Route("api/[controller]/v{version:ApiVersion}")]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private IBookBusiness _bookBusiness;

        /// <summary>
        /// Constructor for the Book controller.
        /// </summary>
        /// <param name="_logger">Logger to register events or problems.</param>
        /// <param name="_bookService">Service for manipulating Book data.</param>
        public BookController(ILogger<BookController> logger, IBookBusiness bookBusiness)
        {
            _logger = logger;
            _bookBusiness = bookBusiness;
        }

        /// <summary>
        /// Gets all books.
        /// </summary>
        /// <returns>A list of books.</returns>
        /// Maps GET requests to https://localhost:44300/api/book
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_bookBusiness.FindAll());
        }

        /// <summary>
        /// Gets a book by its ID.
        /// </summary>
        /// <param name="id">The ID of the book.</param>
        /// <returns>The book with the specified ID.</returns>
        /// Maps GET requests to https://localhost:44300/api/book/{id}
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var book = _bookBusiness.FindById(id);
            if (book == null) return NotFound();
            return Ok(book);
        }

        /// <summary>
        /// Creates a new book.
        /// </summary>
        /// <param name="book">The book to be created.</param>
        /// <returns>The created book.</returns>
        /// Maps POST requests to https://localhost:44300/api/book
        /// [FromBody] tells the framework to serialize the request body to the book instance.
        [HttpPost]
        public IActionResult Post([FromBody] BookVO book)
        {
            if (book == null) return BadRequest();
            return Ok(_bookBusiness.Create(book));
        }

        /// <summary>
        /// Updates an existing book.
        /// </summary>
        /// <param name="book">The book to be updated.</param>
        /// <returns>The updated book.</returns>
        /// Maps PUT requests to https://localhost:44300/api/book
        [HttpPut]
        public IActionResult Put([FromBody] BookVO book)
        {
            if (book == null) return BadRequest();
            return Ok(_bookBusiness.Update(book));
        }

        /// <summary>
        /// Deletes a book by its ID.
        /// </summary>
        /// <param name="id">The ID of the book to be deleted.</param>
        /// <returns>Returns a status indicating that there is no content after the deletion.</returns>
        /// Maps DELETE requests to https://localhost:44300/api/book/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _bookBusiness.Delete(id);
            return NoContent();
        }
    }
}
