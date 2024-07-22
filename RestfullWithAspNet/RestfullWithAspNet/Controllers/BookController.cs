using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestfullWithAspNet.Business;
using RestfullWithAspNet.Data.VO;
using HATEOAS.Hypernedia.Filters;

namespace RestfullWithAspNet.Controllers
{
    /// <summary>
    /// Controller for manipulating book data.
    /// </summary>
    [ApiVersion("1")]
    [ApiController]
    [Authorize("Bearer")]
    [Route("api/[controller]/v{version:ApiVersion}")]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private IBookBusiness _bookBusiness;

        /// <summary>
        /// Constructor for the Book controller.
        /// </summary>
        /// <param name="logger">Logger to register events or problems.</param>
        /// <param name="bookBusiness">Service for manipulating Book data.</param>
        public BookController(ILogger<BookController> logger, IBookBusiness bookBusiness)
        {
            _logger = logger;
            _bookBusiness = bookBusiness;
        }

        /// <summary>
        /// Gets all books.
        /// </summary>
        /// <returns>A list of books.</returns>
        /// <remarks>
        /// Sample request:
        ///     GET /api/book
        /// </remarks>
        [HttpGet("{srtDirection}/{pageSize}/{page}")]
        [ProducesResponseType((200), Type = typeof(List<BookVO>))]
        [ProducesResponseType((204))]
        [ProducesResponseType((400))]
        [ProducesResponseType((401))]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get(
            [FromQuery] string? title,
            string srtDirection,
            int pageSize,
            int page)
        {
            return Ok(_bookBusiness.FindWithPagedSearch(title, srtDirection, pageSize, page));
        }


        /// <summary>
        /// Gets a book by its ID.
        /// </summary>
        /// <param name="id">The ID of the book.</param>
        /// <returns>The book with the specified ID.</returns>
        /// <remarks>
        /// Sample request:
        ///     GET /api/book/{id}
        /// </remarks>
        [HttpGet("{id}")]
        [ProducesResponseType((200), Type = typeof(BookVO))]
        [ProducesResponseType((204))]
        [ProducesResponseType((400))]
        [ProducesResponseType((401))]
        [TypeFilter(typeof(HyperMediaFilter))]
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
        /// <remarks>
        /// Sample request:
        ///     POST /api/book
        ///     {
        ///         "title": "Sample Book",
        ///         "author": "John Doe"
        ///     }
        /// </remarks>
        [HttpPost]
        [ProducesResponseType((200), Type = typeof(BookVO))]
        [ProducesResponseType((400))]
        [ProducesResponseType((401))]
        [TypeFilter(typeof(HyperMediaFilter))]
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
        /// <remarks>
        /// Sample request:
        ///     PUT /api/book
        ///     {
        ///         "id": 1,
        ///         "title": "Updated Book",
        ///         "author": "Jane Doe"
        ///     }
        /// </remarks>
        [HttpPut]
        [ProducesResponseType((200), Type = typeof(BookVO))]
        [ProducesResponseType((204))]
        [ProducesResponseType((400))]
        [ProducesResponseType((401))]
        [TypeFilter(typeof(HyperMediaFilter))]
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
        /// <remarks>
        /// Sample request:
        ///     DELETE /api/book/{id}
        /// </remarks>
        [HttpDelete("{id}")]
        [ProducesResponseType((204))]
        [ProducesResponseType((400))]
        [ProducesResponseType((401))]
        public IActionResult Delete(long id)
        {
            _bookBusiness.Delete(id);
            return NoContent();
        }
    }
}
