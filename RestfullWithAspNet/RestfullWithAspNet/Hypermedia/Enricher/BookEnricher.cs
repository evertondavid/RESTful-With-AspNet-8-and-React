using System.Text;
using Microsoft.AspNetCore.Mvc;
using RestfullWithAspNet.Data.VO;
using RestfullWithAspNet.Hypermedia.Constants;
using RestfullWithAspNet.Hypermedia.Filters;

namespace RestfullWithAspNet.Hypernedia.Enricher
{
    /// <summary>
    /// Enriches the BookVO model with hypermedia links.
    /// </summary>
    public class BookEnricher : ContentResponseEnricher<BookVO>
    {

        /// <summary>
        /// Enriches the BookVO model with hypermedia links.
        /// </summary>
        /// <param name="content">The BookVO model to enrich.</param>
        /// <param name="urlHelper">The IUrlHelper instance for generating URLs.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        protected override Task EnrichModel(BookVO content, IUrlHelper urlHelper)
        {
            var path = "api/book";
            string link = GetLink(content.Id, urlHelper, path);

            // Add hypermedia links to the BookVO model
            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.GET,
                Href = link,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultGet
            });
            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.POST,
                Href = link,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultPost
            });
            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.PUT,
                Href = link,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultPut
            });
            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.DELETE,
                Href = link,
                Rel = RelationType.self,
                Type = "int"
            });
            return Task.CompletedTask;
        }

        /// <summary>
        /// Gets the link for the specified id and path.
        /// </summary>
        /// <param name="id">The id of the BookVO model.</param>
        /// <param name="urlHelper">The IUrlHelper instance for generating URLs.</param>
        /// <param name="path">The path for the link.</param>
        /// <returns>The generated link.</returns>
        private string GetLink(long id, IUrlHelper urlHelper, string path)
        {
            lock (this)
            {
                var url = new { controller = path, id };
                return new StringBuilder(urlHelper.Link("DefaultApi", url)).Replace("%2F", "/").ToString();
            }
        }
    }
}
