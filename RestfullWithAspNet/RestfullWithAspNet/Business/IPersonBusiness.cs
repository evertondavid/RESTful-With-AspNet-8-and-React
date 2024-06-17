using RestfullWithAspNet.Data.VO;
using System.Collections.Generic; // Added using statement for List<T>

namespace RestfullWithAspNet.Business
{
    /// <summary>
    /// Interface for the Business service.
    /// </summary>
    public interface IPersonBusiness
    {
        /// <summary>
        /// Creates a new person.
        /// </summary>
        /// <param name="person">The person to be created.</param>
        /// <returns>The person created.</returns>
        PersonVO Create(PersonVO person);

        /// <summary>
        /// Find a person by its ID.
        /// </summary>
        /// <param name="id">The ID of the person.</param>
        /// <returns>The person with the specific ID.</returns>
        PersonVO FindById(long id);

        /// <summary>
        /// Find all people.
        /// </summary>
        /// <returns>List of people.</returns>
        List<PersonVO> FindAll(); // Added return type List<PersonVO>

        /// <summary>
        /// Updates a person.
        /// </summary>
        /// <param name="person">The person to be updated.</param>
        /// <returns>The person updated.</returns>
        PersonVO Update(PersonVO person);

        /// <summary>
        /// Delete a person by its ID.
        /// </summary>
        /// <param name="id">The ID of the person to be deleted.</param>
        void Delete(long id);
    }
}
