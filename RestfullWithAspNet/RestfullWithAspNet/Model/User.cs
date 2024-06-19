using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestfullWithAspNet.Model
{
    /// <summary>
    /// Represents a user in the system.
    /// </summary>
    [Table("users")]
    public class User
    {
        /// <summary>
        /// Gets or sets the ID of the user.
        /// </summary>
        [Key]
        [Column("id")]
        public long? Id { get; set; }

        /// <summary>
        /// Gets or sets the username of the user.
        /// </summary>
        [Column("user_name")]
        public string? UserName { get; set; }

        /// <summary>
        /// Gets or sets the full name of the user.
        /// </summary>
        [Column("full_name")]
        public string? FullName { get; set; }

        /// <summary>
        /// Gets or sets the password of the user.
        /// </summary>
        [Column("password")]
        public string? Password { get; set; }

        /// <summary>
        /// Gets or sets the refresh token of the user.
        /// </summary>
        [Column("refresh_token")]
        public string? RefreshToken { get; set; }

        /// <summary>
        /// Gets or sets the expiry time of the refresh token.
        /// </summary>
        [Column("refresh_token_expiry_time")]
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
