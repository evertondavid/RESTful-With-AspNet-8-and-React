using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using RestfullWithAspNet.Business;
using RestfullWithAspNet.Model;

namespace RestfullWithAspNet.Controllers
{
    /// <summary>
    /// Controlador para manipulação de dados de book.
    /// </summary>
    [ApiVersion("1")]
    [ApiController]
    [Route("api/[controller]/v{version:ApiVersion}")]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private IBookBusiness _bookBusiness;

        /// <summary>
        /// Construtor do controlador Book.
        /// </summary>
        /// <param name="_logger">Logger para registrar eventos ou problemas.</param>
        /// <param name="_bookService">Serviço para manipulação de dados de Book.</param>
        public BookController(ILogger<BookController> logger, IBookBusiness bookBusiness)
        {
            _logger = logger;
            _bookBusiness = bookBusiness;
        }

        /// <summary>
        /// Obtém todas as livros.
        /// </summary>
        /// <returns>Uma lista de livros.</returns>
        /// Maps GET requests to https://localhost:44300/api/book
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_bookBusiness.FindAll());
        }

        /// <summary>
        /// Obtém uma pessoa pelo seu ID.
        /// </summary>
        /// <param name="id">O ID da pessoa.</param>
        /// <returns>A pessoa com o ID especificado.</returns>
        /// Maps GET requests to https://localhost:44300/api/book/{id}
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var book = _bookBusiness.FindById(id);
            if (book == null) return NotFound();
            return Ok(book);
        }

        /// <summary>
        /// Cria uma nova pessoa.
        /// </summary>
        /// <param name="book">A pessoa a ser criada.</param>
        /// <returns>A pessoa criada.</returns>
        /// Maps POST requests to https://localhost:44300/api/book
        /// [FromBody] tells the framework to serialize the request body to the book instance.
        [HttpPost]
        public IActionResult Post([FromBody] Book book)
        {
            if (book == null) return BadRequest();
            return Ok(_bookBusiness.Create(book));
        }

        /// <summary>
        /// Atualiza uma pessoa existente.
        /// </summary>
        /// <param name="book">A pessoa a ser atualizada.</param>
        /// <returns>A pessoa atualizada.</returns>
        /// Maps PUT requests to https://localhost:44300/api/book
        [HttpPut]
        public IActionResult Put([FromBody] Book book)
        {
            if (book == null) return BadRequest();
            return Ok(_bookBusiness.Update(book));
        }

        /// <summary>
        /// Deleta uma pessoa pelo seu ID.
        /// </summary>
        /// <param name="id">O ID da pessoa a ser deletada.</param>
        /// <returns>Retorna um status indicando que não há conteúdo após a exclusão.</returns>
        /// Maps DELETE requests to https://localhost:44300/api/book/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _bookBusiness.Delete(id);
            return NoContent();
        }
    }
}
