using System.Security.Claims;

namespace RestfullWithAspNet.Services
{
    /// <summary>
    /// Represents a service for generating and validating tokens.
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Generates an access token based on the provided claims.
        /// </summary>
        /// <param name="claims">The claims to include in the access token.</param>
        /// <returns>The generated access token.</returns>
        string GenerateAccessToken(IEnumerable<Claim> claims);

        /// <summary>
        /// Generates a refresh token.
        /// </summary>
        /// <returns>The generated refresh token.</returns>
        string GenerateRefreshToken();

        /// <summary>
        /// Retrieves the claims principal from an expired token.
        /// </summary>
        /// <param name="token">The expired token.</param>
        /// <returns>The claims principal extracted from the expired token.</returns>
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
