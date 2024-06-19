using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RestfullWithAspNet.Hypernedia.Filters
{
    /// <summary>
    /// Represents a filter that enriches the result of an action with hypermedia links.
    /// </summary>
    public class HyperMediaFilter : ResultFilterAttribute
    {
        private readonly HyperMediaFilterOptions _hyperMediaFilterOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="HyperMediaFilter"/> class.
        /// </summary>
        /// <param name="hyperMediaFilterOptions">The options for the hypermedia filter.</param>
        public HyperMediaFilter(HyperMediaFilterOptions hyperMediaFilterOptions)
        {
            _hyperMediaFilterOptions = hyperMediaFilterOptions;
        }

        /// <summary>
        /// Called before the action result is executed.
        /// </summary>
        /// <param name="context">The context for the result executing.</param>
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            TryEnrichResult(context);
            base.OnResultExecuting(context);
        }

        private void TryEnrichResult(ResultExecutingContext context)
        {
            if (context.Result is OkObjectResult objectResult)
            {
                var enrichers = _hyperMediaFilterOptions
                .ContentResponseEnricherList
                .FirstOrDefault(x => x.CanEnrich(context));
                if (enrichers != null) Task.FromResult(enrichers.Enrich(context));
            }
        }
    }
}
