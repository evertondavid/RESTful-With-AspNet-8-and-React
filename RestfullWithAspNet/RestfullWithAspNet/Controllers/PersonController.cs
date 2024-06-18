using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using RestfullWithAspNet.Business;
using RestfullWithAspNet.Data.VO;
using RestfullWithAspNet.Hypernedia.Filters;

namespace RestfullWithAspNet.Controllers
{
    /// <summary>
    /// Controller for manipulating Person data.
    /// </summary>
    [ApiVersion("1")]
    [ApiController]
    [Route("api/[controller]/v{version:ApiVersion}")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private IPersonBusiness _personBusiness;

        /// <summary>
        /// Constructor for the Person controller.
        /// </summary>
        /// <param name="logger">Logger to register events or problems.</param>
        /// <param name="personBusiness">Service for manipulating Person data.</param>
        public PersonController(ILogger<PersonController> logger, IPersonBusiness personBusiness)
        {
            _logger = logger;
            _personBusiness = personBusiness;
        }

        /// <summary>
        /// Gets all people.
        /// </summary>
        /// <returns>A list of people.</returns>
        /// <remarks>
        /// Maps GET requests to https://localhost:44300/api/person
        /// </remarks>
        [HttpGet]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get()
        {
            return Ok(_personBusiness.FindAll());
        }

        /// <summary>
        /// Gets a person by their ID.
        /// </summary>
        /// <param name="id">The ID of the person.</param>
        /// <returns>The person with the specified ID.</returns>
        /// <remarks>
        /// Maps GET requests to https://localhost:44300/api/person/{id}
        /// </remarks>
        [HttpGet("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get(long id)
        {
            var person = _personBusiness.FindById(id);
            if (person == null) return NotFound();
            return Ok(person);
        }

        /// <summary>
        /// Creates a new person.
        /// </summary>
        /// <param name="person">The person to be created.</param>
        /// <returns>The created person.</returns>
        /// <remarks>
        /// Maps POST requests to https://localhost:44300/api/person
        /// [FromBody] tells the framework to serialize the request body to the person instance.
        /// </remarks>
        [HttpPost]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Post([FromBody] PersonVO person)
        {
            if (person == null) return BadRequest();
            return Ok(_personBusiness.Create(person));
        }

        /// <summary>
        /// Updates an existing person.
        /// </summary>
        /// <param name="person">The person to be updated.</param>
        /// <returns>The updated person.</returns>
        /// <remarks>
        /// Maps PUT requests to https://localhost:44300/api/person
        /// </remarks>
        [HttpPut]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Put([FromBody] PersonVO person)
        {
            if (person == null) return BadRequest();
            return Ok(_personBusiness.Update(person));
        }

        /// <summary>
        /// Deletes a person by their ID.
        /// </summary>
        /// <param name="id">The ID of the person to be deleted.</param>
        /// <returns>Returns a status indicating that there is no content after the deletion.</returns>
        /// <remarks>
        /// Maps DELETE requests to https://localhost:44300/api/person/{id}
        /// </remarks>
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _personBusiness.Delete(id);
            return NoContent();
        }
    }
}
