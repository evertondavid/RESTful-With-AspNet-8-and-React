using RestfullWithAspNet.Data.VO;
using HATEOAS.Hypermedia.Utils;

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
        /// Find people by name.
        /// </summary>
        /// <param name="firstName">The first name to search for.</param>
        /// <param name="lastName">The last name to search for.</param>
        /// <returns>A list of people matching the specified first name and last name.</returns>
        List<PersonVO> FindByName(string firstName, string lastName);

        /// <summary>
        /// Find all people.
        /// </summary>
        /// <returns>List of people.</returns>
        List<PersonVO> FindAll(); // Added return type List<PersonVO>

        /// <summary>
        /// Find people with a paged search.
        /// </summary>
        /// <param name="sortDirection">The sort direction.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="page">The page number.</param>
        /// <returns>A paged search result.</returns>
        PagedSearchVO<PersonVO> FindWithPagedSearch(string sortDirection, int pageSize, int page);

        /// <summary>
        /// Find people with a paged search.
        /// </summary>
        /// <param name="name">The name to search for.</param>
        /// <param name="sortDirection">The sort direction.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="page">The page number.</param>
        /// <returns>A paged search result.</returns>
        PagedSearchVO<PersonVO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page);

        /// <summary>
        /// Updates a person.
        /// </summary>
        /// <param name="person">The person to be updated.</param>
        /// <returns>The person updated.</returns>
        PersonVO Update(PersonVO person);

        /// <summary>
        /// Disables a person.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        PersonVO Disable(long id);

        /// <summary>
        /// Delete a person by its ID.
        /// </summary>
        /// <param name="id">The ID of the person to be deleted.</param>
        void Delete(long id);
    }
}
