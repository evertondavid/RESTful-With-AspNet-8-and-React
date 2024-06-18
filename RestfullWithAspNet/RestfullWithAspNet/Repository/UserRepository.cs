using System.Security.Cryptography;
using System.Text;
using RestfullWithAspNet.Data.VO;
using RestfullWithAspNet.Model;
using RestfullWithAspNet.Model.Context;

namespace RestfullWithAspNet.Repository
{
    /// <summary>
    /// Represents a repository for managing user data.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly MySQLContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="context">The MySQL context.</param>
        public UserRepository(MySQLContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Validates the credentials of a user.
        /// </summary>
        /// <param name="user">The user credentials.</param>
        /// <returns>The validated user or null if the credentials are invalid.</returns>
        public User ValidateCredentials(UserVO user)
        {
            var pass = ComputeHash(user.Password, SHA256.Create());
            return _context.Users.FirstOrDefault(u => (u.UserName == user.UserName) && (u.Password == pass));
        }

        /// <summary>
        /// Validates the credentials of a user.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public User ValidateCredentials(string userName)
        {
            return _context.Users.SingleOrDefault(u => u.UserName == userName);
        }

        /// <summary>
        /// Revokes the token of a user.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool RevokeToken(string userName)
        {
            var user = _context.Users.SingleOrDefault(u => u.UserName == userName);
            if (user == null) return false;

            user.RefreshToken = null;
            //user.RefreshTokenExpiryTime = null;
            _context.SaveChanges();
            return true;
        }

        /// <summary>
        /// Updates an existing user entity in the repository.
        /// </summary>
        /// <param name="user">The user entity to update.</param>
        /// <returns>The updated user entity or null if the user does not exist.</returns>
        public User RefreshUserInfo(User user)
        {
            if (!_context.Users.Any(u => u.Id.Equals(user.Id))) return null;

            var result = _context.Users.SingleOrDefault(p => p.Id.Equals(user.Id));
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(user);
                    _context.SaveChanges();
                    return result;
                }
                catch
                {
                    throw; // Preserve the original exception
                }
            }
            else
            {
                return result;
            }
        }

        /// <summary>
        /// Computes the hash of a string using a specified algorithm.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="algorithm"></param>
        /// <returns></returns>
        private object ComputeHash(string input, HashAlgorithm algorithm)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashedBytes = algorithm.ComputeHash(inputBytes);

            var builder = new StringBuilder();
            foreach (var item in hashedBytes)
            {
                builder.Append(item.ToString("x2"));
            }
            return builder.ToString();
        }
    }
}
