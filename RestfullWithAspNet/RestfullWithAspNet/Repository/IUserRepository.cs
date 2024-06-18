using RestfullWithAspNet.Data.VO;
using RestfullWithAspNet.Model;

namespace RestfullWithAspNet.Repository
{
    /// <summary>
    /// Represents the interface for user repository.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Validates the credentials of a user.
        /// </summary>
        /// <param name="user">The user object containing the credentials to validate.</param>
        /// <returns>The validated user object if the credentials are valid, otherwise null.</returns>
        User ValidateCredentials(UserVO user);

        /// <summary>
        /// Validates the credentials of a user.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        User ValidateCredentials(string username);

        /// <summary>
        /// Revokes the token of a user.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        bool RevokeToken(string username);

        /// <summary>
        /// Refreshes the user information.
        /// </summary>
        /// <param name="user">The user object to refresh.</param>
        /// <returns>The refreshed user object if successful, otherwise null.</returns>
        User RefreshUserInfo(User user);
    }
}
