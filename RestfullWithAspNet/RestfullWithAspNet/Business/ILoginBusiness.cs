using RestfullWithAspNet.Data.VO;

namespace RestfullWithAspNet.Business
{
    /// <summary>
    /// Represents the interface for login business logic.
    /// </summary>
    public interface ILoginBusiness
    {
        /// <summary>
        /// Validates the credentials of a user.
        /// </summary>
        /// <param name="user">The user object containing the credentials to validate.</param>
        /// <returns>The token value object if the credentials are valid, otherwise null.</returns>
        TokenVO? ValidateCredentials(UserVO user);

        /// <summary>
        /// Validates the credentials of a user.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        TokenVO? ValidateCredentials(TokenVO token);

        /// <summary>
        /// Revokes the token of a user.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        bool RevokeToken(string userName);
    }
}
