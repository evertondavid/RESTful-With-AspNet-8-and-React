using System.ComponentModel.DataAnnotations.Schema;
using RestfullWithAspNet.Model.Base;

namespace RestfullWithAspNet.Model
{
    [Table("person")]
    /// <summary>
    /// Classe Person that represents a person.
    /// </summary>
    public class Person : BaseEntity
    {
        /// <summary>
        /// Get or set the person's first name.
        /// </summary>
        private string _firstName = string.Empty;
        [Column("first_name")]
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("First name cannot be null or empty.");
                }
                _firstName = value;
            }
        }
        /// <summary>
        /// Get or set the person's last name.
        /// </summary>
        private string _lastName = string.Empty;
        [Column("last_name")]
        public string LastName
        {
            get => _lastName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Last name cannot be null or empty.");
                }
                _lastName = value;
            }
        }
        /// <summary>
        /// Gets or sets the address. The address should not be null or empty.
        /// </summary>
        private string _address = string.Empty;
        [Column("address")]
        public string Address
        {
            get => _address;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Address cannot be null or empty.");
                }
                _address = value;
            }
        }

        /// <summary>
        /// Get or set the Gender.
        /// </summary>
        private string _gender = string.Empty;
        [Column("gender")]
        public string Gender
        {
            get => _gender;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Gender cannot be null or empty.");
                }
                _gender = value;
            }
        }

        /// <summary>
        /// Get or set the Enabled.
        /// </summary>
        private bool _enabled = true;
        [Column("enabled")]
        public bool Enabled
        {
            get => _enabled;
            set
            {
                if (bool.Equals(value, null))
                {
                    throw new ArgumentException("Enabled cannot be null or empty.");
                }
                _enabled = value;
            }
        }
    }
}
