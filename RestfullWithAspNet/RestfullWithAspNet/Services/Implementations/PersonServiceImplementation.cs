using RestfullWithAspNet.Model;
using RestfullWithAspNet.Model.Context;

namespace RestfullWithAspNet.Services.Implementations
{
    /// <summary>
    /// Provides implementation for the IPersonService interface.
    /// </summary>
    public class PersonServiceImplementation : IPersonService
    {
        private MySQLContext _context;

        /// <summary>
        /// Initializes a new instance of the PersonServiceImplementation class.
        /// </summary>
        /// <param name="context">Database context for accessing persons.</param>
        public PersonServiceImplementation(MySQLContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Retrieves all persons from the database.
        /// </summary>
        /// <returns>A list of all persons.</returns>
        public List<Person> FindAll()
        {
            return _context.Persons.ToList();
        }
        /// <summary>
        /// Finds a person by their ID.
        /// </summary>
        /// <param name="id">The ID of the person to find.</param>
        /// <returns>The person with the specified ID, or throws a KeyNotFoundException if not found.</returns>
        public Person FindById(long id)
        {
            var person = _context.Persons.SingleOrDefault(p => p.Id.Equals(id));
            return person ?? throw new KeyNotFoundException($"The person with ID: {id} was not found.");
        }
        /// <summary>
        /// Creates a new person in the database.
        /// </summary>
        /// <param name="person">The person to create.</param>
        /// <returns>The created person.</returns>
        public Person Create(Person person)
        {
            try
            {
                _context.Add(person);
                _context.SaveChanges();
            }
            catch
            {
                throw; // Preserve the original exception
            }
            return person;
        }
        /// <summary>
        /// Updates an existing person in the database.
        /// </summary>
        /// <param name="person">The person to update.</param>
        /// <returns>The updated person, or a new Person if the original does not exist.</returns>
        public Person Update(Person person)
        {
            if (!Exists(person.Id)) return new Person();
            var result = _context.Persons.SingleOrDefault(p => p.Id.Equals(person.Id));
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(person);
                    _context.SaveChanges();
                }
                catch
                {
                    throw; // Preserve the original exception
                }
            }
            return person;
        }
        /// <summary>
        /// Deletes a person from the database by their ID.
        /// </summary>
        /// <param name="id">The ID of the person to delete.</param>
        public void Delete(long id)
        {
            var result = _context.Persons.SingleOrDefault(p => p.Id.Equals(id));
            if (result != null)
            {
                try
                {
                    _context.Persons.Remove(result);
                    _context.SaveChanges();
                }
                catch
                {
                    throw; // Preserve the original exception
                }
            }
        }
        /// <summary>
        /// Checks if a person exists in the database by their ID.
        /// </summary>
        /// <param name="id">The ID of the person to check.</param>
        /// <returns>True if the person exists, false otherwise.</returns>
        private bool Exists(long id)
        {
            return _context.Persons.Any(p => p.Id.Equals(id));
        }
    }
}
