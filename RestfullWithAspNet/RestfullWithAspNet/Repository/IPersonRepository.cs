using RestfullWithAspNet.Model;

namespace RestfullWithAspNet.Repository
{
    /// <summary>
    /// Interface for the Person service.
    /// </summary>
    //public interface IPersonRepository
    public interface IPersonRepository : IRepository<Person>
    {
        /// <summary>
        /// Enable a person.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Person Disable(long id);
    }
}
