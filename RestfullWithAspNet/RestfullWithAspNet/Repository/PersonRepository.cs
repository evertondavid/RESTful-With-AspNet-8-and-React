using RestfullWithAspNet.Model;
using RestfullWithAspNet.Model.Context;
using RestfullWithAspNet.Repository.Generic;

namespace RestfullWithAspNet.Repository
{
    /// <summary>
    /// Repository class for managing Person entities.
    /// </summary>
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PersonRepository"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public PersonRepository(MySQLContext context) : base(context) { }

        /// <summary>
        /// Disables a person with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the person to disable.</param>
        /// <returns>The disabled person.</returns>
        /// <exception cref="Exception">Thrown when the person with the specified ID is not found.</exception>
        public Person Disable(long id)
        {
            if (!_context.Persons.Any(p => p.Id.Equals(id))) throw new Exception("Entity not found.");

            var user = _context.Persons.SingleOrDefault(p => p.Id.Equals(id));
            if (user == null) throw new Exception("Entity not found.");

            user.Enabled = !user.Enabled;

            try
            {
                _context.Entry(user).CurrentValues.SetValues(user);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return user;
        }

        /// <summary>
        /// Finds persons by their first name and last name.
        /// </summary>
        /// <param name="firstName">The first name to search for.</param>
        /// <param name="lastName">The last name to search for.</param>
        /// <returns>A list of persons matching the specified first name and last name.</returns>
        public List<Person> FindByName(string firstName, string lastName)
        {
            if (!string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(lastName))
                return _context.Persons.Where(
                    p => p.FirstName.Contains(firstName)
                    && p.LastName.Contains(lastName)).ToList();
            else if (string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(lastName))
                return _context.Persons.Where(p => p.LastName.Contains(lastName)).ToList();
            else if (!string.IsNullOrWhiteSpace(firstName) && string.IsNullOrWhiteSpace(lastName))
                return _context.Persons.Where(p => p.FirstName.Contains(firstName)).ToList();
            else
                throw new Exception("Invalid parameters.");
        }
    }
}
