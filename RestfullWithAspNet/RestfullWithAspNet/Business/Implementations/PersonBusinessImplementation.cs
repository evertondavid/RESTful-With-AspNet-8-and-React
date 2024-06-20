using RestfullWithAspNet.Data.Converter.Implementations;
using RestfullWithAspNet.Data.VO;
using RestfullWithAspNet.Hypermedia.Utils;
using RestfullWithAspNet.Repository;

namespace RestfullWithAspNet.Business.Implementations
{
    /// <summary>
    /// Provides implementation for the IPersonBusiness interface using Rules.
    /// </summary>
    public class PersonBusinessImplementation : IPersonBusiness
    {
        private readonly IPersonRepository _repository;
        private readonly PersonConverter _converter;

        /// <summary>
        /// Initializes a new instance of the PersonBusinessImplementation class.
        /// </summary>
        /// <param name="repository">The repository for accessing persons.</param>
        public PersonBusinessImplementation(IPersonRepository repository)
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
        /// Retrieves all persons from the database with a paged search.
        /// </summary>
        /// <param name="sortDirection">The sort direction.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="page">The page number.</param>
        /// <returns>A paged search result.</returns>
        /// <remarks>
        /// This method is implemented in the repository, but the logic is here.
        /// </remarks>
        public PagedSearchVO<PersonVO> FindWithPagedSearch(string sortDirection, int pageSize, int page)
        {
            var name = "";
            var sort = (!string.IsNullOrWhiteSpace(sortDirection) && !sortDirection.Equals("desc")) ? "asc" : "desc"; // Default to ascending if not specified or invalid
            var size = (pageSize < 1) ? 10 : pageSize;
            var offset = page > 0 ? (page - 1) * size : 0;

            var query = @"select * from person p where 1 = 1";
            if (!string.IsNullOrWhiteSpace(name)) query += $" and p.first_name like '%{name}%'";
            query += $" order by p.first_name {sort} limit {size} offset {offset}";

            var countQuery = @"select count(*) from person p where 1 = 1";
            if (!string.IsNullOrWhiteSpace(name)) countQuery += $" and p.first_name like '%{name}%'";

            var persons = _repository.FindWithPagedSearch(query);
            int totalResults = _repository.GetCount(countQuery);

            return new PagedSearchVO<PersonVO>
            {
                CurrentPage = page,
                List = _converter.Parse(persons),
                PageSize = size,
                SortDirections = sort,
                TotalResults = totalResults
            };
        }

        /// <summary>
        /// Retrieves all persons from the database with a paged search.
        /// </summary>
        /// <param name="name">The name to search for.</param>
        /// <param name="sortDirection">The sort direction.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="page">The page number.</param>
        /// <returns>A paged search result.</returns>
        /// <remarks>
        /// This method is implemented in the repository, but the logic is here.
        /// </remarks>
        public PagedSearchVO<PersonVO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page)
        {
            var sort = (!string.IsNullOrWhiteSpace(sortDirection) && !sortDirection.Equals("desc")) ? "asc" : "desc"; // Default to ascending if not specified or invalid
            var size = (pageSize < 1) ? 10 : pageSize;
            var offset = page > 0 ? (page - 1) * size : 0;

            var query = @"select * from person p where 1 = 1";
            if (!string.IsNullOrWhiteSpace(name)) query += $" and p.first_name like '%{name}%'";
            query += $" order by p.first_name {sort} limit {size} offset {offset}";

            var countQuery = @"select count(*) from person p where 1 = 1";
            if (!string.IsNullOrWhiteSpace(name)) countQuery += $" and p.first_name like '%{name}%'";

            var persons = _repository.FindWithPagedSearch(query);
            int totalResults = _repository.GetCount(countQuery);

            return new PagedSearchVO<PersonVO>
            {
                CurrentPage = page,
                List = _converter.Parse(persons),
                PageSize = size,
                SortDirections = sort,
                TotalResults = totalResults
            };
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
        /// Finds people by their first name and last name.
        /// </summary>
        /// <param name="firstName">The first name to search for.</param>
        /// <param name="lastName">The last name to search for.</param>
        /// <returns>A list of people matching the specified first name and last name.</returns>
        public List<PersonVO> FindByName(string firstName, string lastName)
        {
            return _converter.Parse(_repository.FindByName(firstName, lastName));
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
        /// Disables a person in the database.
        /// </summary>
        /// <param name="id">The ID of the person to disable.</param>
        /// <returns>The disabled person, or a new Person if the original does not exist.</returns>
        public PersonVO Disable(long id)
        {
            var personEntity = _repository.Disable(id); // Disable the person in the database
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
