using System.Collections.Concurrent;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using RestfullWithAspNet.Hypermedia.Abstract;
using RestfullWithAspNet.Hypermedia.Utils;

namespace RestfullWithAspNet.Hypermedia.Filters
{
    /// <summary>
    /// Base class for enriching content responses with hypermedia.
    /// </summary>
    /// <typeparam name="T">The type of content that supports hypermedia.</typeparam>
    public abstract class ContentResponseEnricher<T> : IResponseEnricher where T : ISupportsHyperMedia
    {

        /// <summary>
        /// Initializes a new instance of the ContentResponseEnricher class.
        /// </summary>
        public ContentResponseEnricher()
        {
        }

        /// <summary>
        /// Determines if the enricher can enrich the specified content type.
        /// </summary>
        /// <param name="contentType">The type of content to enrich.</param>
        /// <returns>True if the enricher can enrich the content type, otherwise false.</returns>
        public bool CanEnrich(Type contentType)
        {
            return contentType == typeof(T) || contentType == typeof(List<T>) || contentType == typeof(PagedSearchVO<T>);
        }

        /// <summary>
        /// Enriches the model with hypermedia links.
        /// </summary>
        /// <param name="content">The content to enrich.</param>
        /// <param name="urlHelper">The URL helper for generating hypermedia links.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        protected abstract Task EnrichModel(T content, IUrlHelper urlHelper);

        /// <summary>
        /// Determines if the enricher can enrich the response.
        /// </summary>
        /// <param name="response">The response to enrich.</param>
        /// <returns>True if the enricher can enrich the response, otherwise false.</returns>
        /// <remarks>Added if block for OkObjectResult, else if block for List<T>, and else if block for PagedSearchVO<T>.</remarks>
        bool IResponseEnricher.CanEnrich(ResultExecutingContext response)
        {
            if (response.Result is OkObjectResult okObjectResult && okObjectResult.Value != null)
            {
                return CanEnrich(okObjectResult.Value.GetType());
            }
            return false;
        }

        /// <summary>
        /// Enriches the response with hypermedia links.
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public async Task Enrich(ResultExecutingContext response)
        {
            var urlHelper = new UrlHelperFactory().GetUrlHelper(response);
            if (response.Result is OkObjectResult okObjectResult) // Added if block for OkObjectResult
            {
                if (okObjectResult.Value is T model)
                {
                    await EnrichModel(model, urlHelper);
                }
                else if (okObjectResult.Value is List<T> collection) // Added else if block for List<T>
                {
                    ConcurrentBag<T> bag = new ConcurrentBag<T>(collection);
                    Parallel.ForEach(bag, (element =>
                    {
                        EnrichModel(element, urlHelper);
                    }
                    ));
                }
                else if (okObjectResult.Value is PagedSearchVO<T> pagedSearch) // Added else if block for PagedSearchVO<T>
                {
                    Parallel.ForEach(pagedSearch.List.ToList(), (element =>
                    {
                        EnrichModel(element, urlHelper);
                    }
                    ));
                }
                await Task.FromResult(0);
            }
        }
    }
}
