using RestfullWithAspNet.Model;
using RestfullWithAspNet.Model.Context;
using RestfullWithAspNet.Repository;

namespace RestfullWithAspNet.Business.Implementations
{
    /// <summary>
    /// Provides implementation for the IPersonBusiness interface using Rules.
    /// </summary>
    public class PersonBusinessImplementation : IPersonBusiness
    {
        private readonly IPersonRepository _repository;

        /// <summary>
        /// Initializes a new instance of the PersonServiceImplementation class.
        /// </summary>
        /// <param name="context">Database context for accessing persons.</param>
        public PersonBusinessImplementation(IPersonRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Retrieves all persons from the database.
        /// </summary>
        /// <returns>A list of all persons.</returns>
        public List<Person> FindAll()
        {
            return _repository.FindAll();
        }

        /// <summary>
        /// Finds a person by their ID.
        /// </summary>
        /// <param name="id">The ID of the person to find.</param>
        /// <returns>The person with the specified ID, or throws a KeyNotFoundException if not found.</returns>
        public Person FindById(long id)
        {
            var person = _repository.FindById(id);
            return person ?? throw new KeyNotFoundException($"The person with ID: {id} was not found.");
        }

        /// <summary>
        /// Creates a new person in the database.
        /// </summary>
        /// <param name="person">The person to create.</param>
        /// <returns>The created person.</returns>
        public Person Create(Person person)
        {
            return _repository.Create(person);
        }

        /// <summary>
        /// Updates an existing person in the database.
        /// </summary>
        /// <param name="person">The person to update.</param>
        /// <returns>The updated person, or a new Person if the original does not exist.</returns>
        public Person Update(Person person)
        {
            return _repository.Update(person);
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
