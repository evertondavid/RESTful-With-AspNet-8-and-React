using Microsoft.AspNetCore.Mvc;
using RestfullWithAspNet.Model;
using RestfullWithAspNet.Services;

namespace RestfullWithAspNet.Controllers
{
    /// <summary>
    /// Controlador para manipulação de dados de Person.
    /// </summary>
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private IPersonService _personService;

        /// <summary>
        /// Construtor do controlador Person.
        /// </summary>
        /// <param name="_logger">Logger para registrar eventos ou problemas.</param>
        /// <param name="_personService">Serviço para manipulação de dados de Person.</param>
        public PersonController(ILogger<PersonController> logger, IPersonService personService)
        {
            _logger = logger;
            _personService = personService;
        }

        /// <summary>
        /// Obtém todas as pessoas.
        /// </summary>
        /// <returns>Uma lista de pessoas.</returns>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_personService.FindAll());
        }

        /// <summary>
        /// Obtém uma pessoa pelo seu ID.
        /// </summary>
        /// <param name="id">O ID da pessoa.</param>
        /// <returns>A pessoa com o ID especificado.</returns>
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var person = _personService.FindById(id);
            if (person == null) return NotFound();
            return Ok(person);
        }

        /// <summary>
        /// Cria uma nova pessoa.
        /// </summary>
        /// <param name="person">A pessoa a ser criada.</param>
        /// <returns>A pessoa criada.</returns>
        [HttpPost]
        public IActionResult Post([FromBody] Person person)
        {
            if (person == null) return BadRequest();
            return Ok(_personService.Create(person));
        }

        /// <summary>
        /// Atualiza uma pessoa existente.
        /// </summary>
        /// <param name="person">A pessoa a ser atualizada.</param>
        /// <returns>A pessoa atualizada.</returns>
        [HttpPut]
        public IActionResult Put([FromBody] Person person)
        {
            if (person == null) return BadRequest();
            return Ok(_personService.Update(person));
        }

        /// <summary>
        /// Deleta uma pessoa pelo seu ID.
        /// </summary>
        /// <param name="id">O ID da pessoa a ser deletada.</param>
        /// <returns>Retorna um status indicando que não há conteúdo após a exclusão.</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _personService.Delete(id);
            return NoContent();
        }
    }
}
