using RestfullWithAspNet.Model;

namespace RestfullWithAspNet.Repository
{
    /// <summary>
    /// Interface for the Person service.
    /// </summary>
    public interface IPersonRepository
    {
        /// <summary>
        /// Creates a new person.
        /// </summary>
        /// <param name="person">The person to be created.</param>
        /// <returns>The person created.</returns>
        Person Create(Person person);

        /// <summary>
        /// Find a person by its ID.
        /// </summary>
        /// <param name="id">The ID of the person.</param>
        /// <returns>The person with the specific ID.</returns>
        Person FindById(long id);

        /// <summary>
        /// Find all people.
        /// </summary>
        /// <returns>List of people.</returns>
        List<Person> FindAll();

        /// <summary>
        /// Updates a person.
        /// </summary>
        /// <param name="person">The person to be updated.</param>
        /// <returns>The person updated.</returns>
        Person Update(Person person);

        /// <summary>
        /// Delete a person by its ID.
        /// </summary>
        /// <param name="id">The ID of the person to be deleted.</param>
        void Delete(long id);

        /// <summary>
        /// Checks if a person exists in the database by their ID.
        /// </summary>
        /// <param name="id">The ID of the person to check.</param>
        /// <returns>True if the person exists, false otherwise.</returns>
        bool Exists(long id);
    }
}
