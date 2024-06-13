namespace RestfullWithAspNet.Model
{
    /// <summary>
    /// Classe Person that represents a person.
    /// </summary>
    public class Person
    {
        /// <summary>
        /// Get or set the person's id.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Get or set the person's first name.
        /// </summary>
        public required string FirstName { get; set; }

        /// <summary>
        /// Get or set the person's last name.
        /// </summary>
        public required string LastName { get; set; }

        /// <summary>
        /// Get or set the person's address.
        /// </summary>
        public required string Address { get; set; }

        /// <summary>
        /// Get or set the Gender.
        /// </summary>
        public required string Gender { get; set; }
    }
}
