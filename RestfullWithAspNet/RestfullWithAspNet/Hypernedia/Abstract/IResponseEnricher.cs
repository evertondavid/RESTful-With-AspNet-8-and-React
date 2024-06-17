using Microsoft.AspNetCore.Mvc.Filters;

namespace RestfullWithAspNet.Hypernedia.Abstract
{
    public interface IResponseEnricher
    {
        bool CanEnrich(ResultExecutingContext context);
        Task Enrich(ResultExecutingContext context);
    }
}
