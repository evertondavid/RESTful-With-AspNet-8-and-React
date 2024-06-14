using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using RestfullWithAspNet.Business;
using RestfullWithAspNet.Model;

namespace RestfullWithAspNet.Controllers
{
    /// <summary>
    /// Controlador para manipulação de dados de Person.
    /// </summary>
    [ApiVersion("1")]
    [ApiController]
    [Route("api/[controller]/v{version:ApiVersion}")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private IPersonBusiness _personBusiness;

        /// <summary>
        /// Construtor do controlador Person.
        /// </summary>
        /// <param name="_logger">Logger para registrar eventos ou problemas.</param>
        /// <param name="_personService">Serviço para manipulação de dados de Person.</param>
        public PersonController(ILogger<PersonController> logger, IPersonBusiness personBusiness)
        {
            _logger = logger;
            _personBusiness = personBusiness;
        }

        /// <summary>
        /// Obtém todas as pessoas.
        /// </summary>
        /// <returns>Uma lista de pessoas.</returns>
        /// Maps GET requests to https://localhost:44300/api/person
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_personBusiness.FindAll());
        }

        /// <summary>
        /// Obtém uma pessoa pelo seu ID.
        /// </summary>
        /// <param name="id">O ID da pessoa.</param>
        /// <returns>A pessoa com o ID especificado.</returns>
        /// Maps GET requests to https://localhost:44300/api/person/{id}
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var person = _personBusiness.FindById(id);
            if (person == null) return NotFound();
            return Ok(person);
        }

        /// <summary>
        /// Cria uma nova pessoa.
        /// </summary>
        /// <param name="person">A pessoa a ser criada.</param>
        /// <returns>A pessoa criada.</returns>
        /// Maps POST requests to https://localhost:44300/api/person
        /// [FromBody] tells the framework to serialize the request body to the person instance.
        [HttpPost]
        public IActionResult Post([FromBody] Person person)
        {
            if (person == null) return BadRequest();
            return Ok(_personBusiness.Create(person));
        }

        /// <summary>
        /// Atualiza uma pessoa existente.
        /// </summary>
        /// <param name="person">A pessoa a ser atualizada.</param>
        /// <returns>A pessoa atualizada.</returns>
        /// Maps PUT requests to https://localhost:44300/api/person
        [HttpPut]
        public IActionResult Put([FromBody] Person person)
        {
            if (person == null) return BadRequest();
            return Ok(_personBusiness.Update(person));
        }

        /// <summary>
        /// Deleta uma pessoa pelo seu ID.
        /// </summary>
        /// <param name="id">O ID da pessoa a ser deletada.</param>
        /// <returns>Retorna um status indicando que não há conteúdo após a exclusão.</returns>
        /// Maps DELETE requests to https://localhost:44300/api/person/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _personBusiness.Delete(id);
            return NoContent();
        }
    }
}
