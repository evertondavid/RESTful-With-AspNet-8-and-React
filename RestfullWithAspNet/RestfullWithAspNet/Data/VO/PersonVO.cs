using HATEOAS.Hypermedia.Abstract;
using HATEOAS.Hypermedia.Filters;

namespace RestfullWithAspNet.Data.VO
{
    /// <summary>
    /// Classe Person that represents a person.
    /// </summary>
    public class PersonVO : ISupportsHyperMedia
    {
        /// <summary>
        /// Get or set the person's ID.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Get or set the person's first name.
        /// </summary>
        private string _firstName = string.Empty;
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

        /// <summary>
        /// Get or set the Hypermedia support.
        /// </summary>
        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }
}
