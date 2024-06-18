using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using RestfullWithAspNet.Configurations;
using RestfullWithAspNet.Data.VO;
using RestfullWithAspNet.Repository;
using RestfullWithAspNet.Services;

namespace RestfullWithAspNet.Business.Implementations
{
    /// <summary>
    /// Implementation of the login business logic.
    /// </summary>
    public class LoginBusinessImplementation : ILoginBusiness
    {
        private const string DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";
        private readonly TokenConfiguration _configuration;
        private readonly ITokenService _tokenService;
        private IUserRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginBusinessImplementation"/> class.
        /// </summary>
        /// <param name="configuration">The token configuration.</param>
        /// <param name="tokenService">The token service.</param>
        /// <param name="repository">The user repository.</param>
        public LoginBusinessImplementation(TokenConfiguration configuration, ITokenService tokenService, IUserRepository repository)
        {
            _configuration = configuration;
            _tokenService = tokenService;
            _repository = repository;
        }

        /// <summary>
        /// Validates the user credentials and generates access and refresh tokens.
        /// </summary>
        /// <param name="userCredentials">The user credentials.</param>
        /// <returns>The token information.</returns>
        public TokenVO ValidateCredentials(UserVO userCredentials)
        {
            var user = _repository.ValidateCredentials(userCredentials);
            if (user == null) return null;
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
            };

            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(_configuration.DaysToExpiry);

            _repository.RefreshUserInfo(user);

            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate.AddMinutes(_configuration.Minutes);

            return new TokenVO(
                true,
                createDate.ToString(DATE_FORMAT),
                expirationDate.ToString(DATE_FORMAT),
                accessToken,
                refreshToken
            );
        }

        /// <summary>
        /// Validates the user credentials and generates new access and refresh tokens using the provided token.
        /// </summary>
        /// <param name="token">The token information.</param>
        /// <returns>The new token information.</returns>
        public TokenVO ValidateCredentials(TokenVO token)
        {
            var accessToken = token.AccessToken;
            var refreshToken = token.RefreshToken;

            var principal = accessToken != null ? _tokenService.GetPrincipalFromExpiredToken(accessToken) : null;
            var username = principal.Identity.Name;
            var user = _repository.ValidateCredentials(username);

            if (user == null ||
            user.RefreshToken != refreshToken ||
            user.RefreshTokenExpiryTime <= DateTime.Now) return null;

            accessToken = _tokenService.GenerateAccessToken(principal.Claims);
            refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;

            _repository.RefreshUserInfo(user);

            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate.AddMinutes(_configuration.Minutes);

            return new TokenVO(
                true,
                createDate.ToString(DATE_FORMAT),
                expirationDate.ToString(DATE_FORMAT),
                accessToken,
                refreshToken
            );
        }

        /// <summary>
        /// Revokes the refresh token for the specified user.
        /// </summary>
        /// <param name="userName">The username.</param>
        /// <returns>True if the token was revoked successfully, otherwise false.</returns>
        public bool RevokeToken(string userName)
        {
            return _repository.RevokeToken(userName);
        }
    }
}
