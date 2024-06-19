
using System.Text.Json.Serialization;

namespace RestfullWithAspNet.Data.VO
{
    /// <summary>
    /// Represents a token value object.
    /// </summary>
    public class TokenVO
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TokenVO"/> class.
        /// </summary>
        /// <param name="authenticated">Indicates whether the token is authenticated.</param>
        /// <param name="created">The creation date of the token.</param>
        /// <param name="expiration">The expiration date of the token.</param>
        /// <param name="accessToken">The access token value.</param>
        /// <param name="refreshToken">The refresh token value.</param>
        [JsonConstructor]
        public TokenVO() { }


        public TokenVO(bool authenticated, string created, string expiration, string accessToken, string refreshToken)
        {
            Authenticated = authenticated;
            Created = created;
            Expiration = expiration;
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the token is authenticated.
        /// </summary>
        [JsonPropertyName("authenticated")]
        public bool? Authenticated { get; set; }

        /// <summary>
        /// Gets or sets the creation date of the token.
        /// </summary>
        [JsonPropertyName("created")]
        public string? Created { get; set; }

        /// <summary>
        /// Gets or sets the expiration date of the token.
        /// </summary>
        [JsonPropertyName("expiration")]
        public string? Expiration { get; set; }

        /// <summary>
        /// Gets or sets the access token value.
        /// </summary>
        [JsonPropertyName("accessToken")]
        public string? AccessToken { get; set; }

        /// <summary>
        /// Gets or sets the refresh token value.
        /// </summary>
        [JsonPropertyName("refreshToken")]
        public string? RefreshToken { get; set; }
    }
}
