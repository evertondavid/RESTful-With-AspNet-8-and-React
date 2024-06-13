using RestfullWithAspNet.Model;

namespace RestfullWithAspNet.Services.Implementations
{
    /// <summary>
    /// Implementation of the IPersonService interface.
    /// </summary>
    public class PersonServiceImplementation : IPersonService
    {
        private static volatile int count;

        /// <summary>
        /// Creates a new person.
        /// </summary>
        /// <param name="person">The person to be created</param>
        /// <returns>The person created</returns>
        public Person Create(Person person)
        {
            return person;
        }

        /// <summary>
        /// Delete a person by its ID.
        /// </summary>
        /// <param name="id">The ID of person to be deleted</param>
        public void Delete(long id)
        {

        }

        /// <summary>
        /// Find all people.
        /// </summary>
        /// <returns>List of people</returns>
        public List<Person> FindAll()
        {
            List<Person> persons = new List<Person>();
            for (int i = 1; i <= 8; i++)
            {
                Person person = MockPerson(i);

                persons.Add(person);
            }
            return persons;
        }

        /// <summary>
        /// Find a person by its ID.
        /// </summary>
        /// <param name="id">The ID of the person</param>
        /// <returns>The person with the specific ID</returns>
        public Person FindById(long id)
        {
            return new Person
            {
                Id = IncrementAndGet(),
                FirstName = "Leandro",
                LastName = "Costa",
                Address = "Uberlândia - Minas Gerais - Brasil",
                Gender = "Male"
            };
        }

        /// <summary>
        /// Update a person by its ID.
        /// </summary>
        /// <param name="person">The person to be updated</param>
        /// <returns>The person updated</returns>
        public Person Update(Person person)
        {
            return person;
        }
        /// <summary>
        /// Creates a mock person.
        /// </summary>
        /// <param name="i"> The index of the mock person</param>
        /// <returns>The mock person</returns>
        private static Person MockPerson(object i)
        {
            return new Person
            {
                Id = IncrementAndGet(),
                FirstName = "Person Name " + i,
                LastName = "Person Last Name",
                Address = "Some Address",
                Gender = "Male"
            };
        }

        /// <summary>
        /// Increment the count of people.
        /// </summary>
        /// <returns>The count of people</returns>
        public static long IncrementAndGet()
        {
            return Interlocked.Increment(ref count);
        }
    }
}
