using System.Text;

namespace RestfullWithAspNet.Hypernedia
{
    /// <summary>
    /// Represents a hyperlink in a hypermedia response.
    /// </summary>
    public class HyperMediaLink
    {
        /// <summary>
        /// Gets or sets the relationship of the hyperlink to the current resource.
        /// </summary>
        public string Rel { get; set; }

        private string href;

        /// <summary>
        /// Gets or sets the target URL of the hyperlink.
        /// </summary>
        public string Href
        {
            get
            {
                object _lock = new object();
                lock (_lock)
                {
                    StringBuilder sb = new StringBuilder(href);
                    return sb.Replace("%2F", "/").ToString();
                }
            }
            set => href = value;
        }

        /// <summary>
        /// Gets or sets the media type of the target resource.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the action associated with the hyperlink.
        /// </summary>
        public string Action { get; set; }
    }
}
