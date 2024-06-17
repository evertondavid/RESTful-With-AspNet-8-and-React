using RestfullWithAspNet.Hypernedia.Abstract;

namespace RestfullWithAspNet.Hypernedia.Filters
{
    public class HyperMediaFilterOptions
    {
        public List<IResponseEnricher> ContentResponseEnricherList { get; set; } = new List<IResponseEnricher>();
    }
}
