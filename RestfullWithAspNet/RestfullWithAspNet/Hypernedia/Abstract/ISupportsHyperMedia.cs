
namespace RestfullWithAspNet.Hypernedia.Abstract
{
    /// <summary>
    /// Represents an interface for supporting hypermedia in a resource.
    /// </summary>
    public interface ISupportsHyperMedia
    {
        /// <summary>
        /// Gets or sets the collection of hypermedia links associated with the resource.
        /// </summary>
        List<HyperMediaLink> Links { get; set; }
    }
}
