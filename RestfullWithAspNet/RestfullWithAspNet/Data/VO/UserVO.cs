// Purpose: Contains the UserVO class which is a Value Object for the User entity.
namespace RestfullWithAspNet.Data.VO
{
    /// <summary>
    /// Represents a User Value Object.
    /// </summary>
    public class UserVO
    {
        /// <summary>
        /// Gets or sets the password of the user.
        /// </summary>
        public required string Password { get; set; }

        /// <summary>
        /// Gets or sets the username of the user.
        /// </summary>
        public required string UserName { get; set; }
    }
}
