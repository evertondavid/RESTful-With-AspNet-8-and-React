using RestfullWithAspNet.Data.Converter.Implementations;
using RestfullWithAspNet.Data.VO;
using RestfullWithAspNet.Model;
using RestfullWithAspNet.Repository;

namespace RestfullWithAspNet.Business.Implementations
{
    /// <summary>
    /// Provides implementation for the IPersonBusiness interface using Rules.
    /// </summary>
    public class PersonBusinessImplementation : IPersonBusiness
    {
        private readonly IRepository<Person> _repository;
        private readonly PersonConverter _converter;

        /// <summary>
        /// Initializes a new instance of the PersonBusinessImplementation class.
        /// </summary>
        /// <param name="repository">The repository for accessing persons.</param>
        public PersonBusinessImplementation(IRepository<Person> repository)
        {
            _repository = repository;
            _converter = new PersonConverter();
        }

        /// <summary>
        /// Retrieves all persons from the database.
        /// </summary>
        /// <returns>A list of all persons.</returns>
        public List<PersonVO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        }

        /// <summary>
        /// Finds a person by their ID.
        /// </summary>
        /// <param name="id">The ID of the person to find.</param>
        /// <returns>The person with the specified ID, or throws a KeyNotFoundException if not found.</returns>
        public PersonVO FindById(long id)
        {
            var person = _repository.FindById(id);
            return _converter.Parse(person) ?? throw new KeyNotFoundException($"The person with ID: {id} was not found.");
        }

        /// <summary>
        /// Creates a new person in the database.
        /// </summary>
        /// <param name="person">The person to create.</param>
        /// <returns>The created person.</returns>
        public PersonVO Create(PersonVO person)
        {
            var personEntity = _converter.Parse(person); // Convert the VO to an Entity
            personEntity = _repository.Create(personEntity); // Create the entity in the database
            return _converter.Parse(personEntity); // Convert the entity back to a VO
        }

        /// <summary>
        /// Updates an existing person in the database.
        /// </summary>
        /// <param name="person">The person to update.</param>
        /// <returns>The updated person, or a new Person if the original does not exist.</returns>
        public PersonVO Update(PersonVO person)
        {
            var personEntity = _converter.Parse(person); // Convert the VO to an Entity
            personEntity = _repository.Update(personEntity); // Update the entity in the database
            return _converter.Parse(personEntity); // Convert the entity back to a VO
        }

        /// <summary>
        /// Deletes a person from the database by their ID.
        /// </summary>
        /// <param name="id">The ID of the person to delete.</param>
        public void Delete(long id)
        {
            _repository.Delete(id);
        }
    }
}
