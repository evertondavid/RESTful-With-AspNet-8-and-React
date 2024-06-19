using RestfullWithAspNet.Hypermedia.Abstract;

namespace RestfullWithAspNet.Hypernedia.Filters
{
    /// <summary>
    /// Represents the options for the HyperMedia filter.
    /// </summary>
    public class HyperMediaFilterOptions
    {
        /// <summary>
        /// Gets or sets the list of content response enrichers.
        /// </summary>
        /// <value>
        /// The list of content response enrichers.
        /// </value>
        public List<IResponseEnricher> ContentResponseEnricherList { get; set; } = new List<IResponseEnricher>();
    }
}
