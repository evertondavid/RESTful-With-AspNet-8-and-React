using RestfullWithAspNet.Model;
using RestfullWithAspNet.Model.Context;
using RestfullWithAspNet.Repository.Generic;

namespace RestfullWithAspNet.Repository
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(MySQLContext context) : base(context) { }

        public Person Disable(long id)
        {
            if (!_context.Persons.Any(p => p.Id.Equals(id))) throw new Exception("Entity not found.");

            var user = _context.Persons.SingleOrDefault(p => p.Id.Equals(id));
            if (user == null) throw new Exception("Entity not found.");
            if (user.Enabled == false)
            {
                user.Enabled = true;
            }
            else
            {
                user.Enabled = false;
            }
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
    }
}
