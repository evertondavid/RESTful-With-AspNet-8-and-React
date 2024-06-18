
namespace RestfullWithAspNet.Hypernedia.Constants
{
    /// <summary>
    /// Represents the response type formats for different HTTP methods.
    /// </summary>
    public sealed class ResponseTypeFormat
    {
        /// <summary>
        /// Gets the default response type format for GET requests.
        /// </summary>
        public const string DefaultGet = "application/json";

        /// <summary>
        /// Gets the default response type format for POST requests.
        /// </summary>
        public const string DefaultPost = "application/json";

        /// <summary>
        /// Gets the default response type format for PUT requests.
        /// </summary>
        public const string DefaultPut = "application/json";

        /// <summary>
        /// Gets the default response type format for PATCH requests.
        /// </summary>
        public const string DefaultDPatch = "application/json";
    }
}
