// Purpose: Contains the configuration settings for token generation and validation.
namespace RestfullWithAspNet.Configurations
{
    /// <summary>
    /// Represents the configuration settings for token generation and validation.
    /// </summary>
    public class TokenConfiguration
    {
        /// <summary>
        /// Gets or sets the audience for the token.
        /// </summary>
        public string? Audience { get; set; }

        /// <summary>
        /// Gets or sets the issuer of the token.
        /// </summary>
        public string? Issuer { get; set; }

        /// <summary>
        /// Gets or sets the secret key used for token generation and validation.
        /// </summary>
        public string? Secret { get; set; }

        /// <summary>
        /// Gets or sets the number of minutes for which the token is valid.
        /// </summary>
        public int Minutes { get; set; }

        /// <summary>
        /// Gets or sets the number of days after which the token will expire.
        /// </summary>
        public int DaysToExpiry { get; set; }
    }
}
