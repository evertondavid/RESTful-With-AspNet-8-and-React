using Microsoft.AspNetCore.Mvc.Filters;

namespace RestfullWithAspNet.Hypermedia.Abstract
{
    /// <summary>
    /// Represents an interface for response enrichers.
    /// </summary>
    public interface IResponseEnricher
    {
        /// <summary>
        /// Determines whether the enricher can enrich the response for the given context.
        /// </summary>
        /// <param name="context">The context of the result execution.</param>
        /// <returns><c>true</c> if the enricher can enrich the response; otherwise, <c>false</c>.</returns>
        bool CanEnrich(ResultExecutingContext context);

        /// <summary>
        /// Enriches the response for the given context.
        /// </summary>
        /// <param name="context">The context of the result execution.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task Enrich(ResultExecutingContext context);
    }
}
