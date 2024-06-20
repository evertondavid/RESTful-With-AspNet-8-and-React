using RestfullWithAspNet.Hypermedia.Abstract;

namespace RestfullWithAspNet.Hypermedia.Utils
{
    public class PagedSearchVO<T> where T : ISupportsHyperMedia
    {
        /// <summary>
        /// Gets or sets the current page number.
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Gets or sets the page size.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets the total number of results.
        /// </summary>
        public long TotalResults { get; set; }

        /// <summary>
        /// Gets or sets the sort fields.
        /// </summary>
        public string? SortFields { get; set; }

        /// <summary>
        /// Gets or sets the sort directions.
        /// </summary>
        public string? SortDirections { get; set; }

        /// <summary>
        /// Gets or sets the filters.
        /// </summary>
        public Dictionary<string, object>? Filters { get; set; }

        /// <summary>
        /// Gets or sets the list of items.
        /// </summary>
        public List<T> List { get; set; } = new List<T>();

        /// <summary>
        /// Default constructor.
        /// </summary>
        public PagedSearchVO()
        {
        }

        /// <summary>
        /// Constructor with required parameters.
        /// </summary>
        /// <param name="currentPage">The current page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortFields">The sort fields.</param>
        /// <param name="sortDirections">The sort directions.</param>
        public PagedSearchVO(int currentPage, int pageSize, string sortFields, string sortDirections)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            SortFields = sortFields;
            SortDirections = sortDirections;
        }

        /// <summary>
        /// Constructor with required parameters and filters.
        /// </summary>
        /// <param name="currentPage">The current page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortFields">The sort fields.</param>
        /// <param name="sortDirections">The sort directions.</param>
        /// <param name="filters">The filters.</param>
        public PagedSearchVO(int currentPage, int pageSize, string sortFields, string sortDirections, Dictionary<string, object> filters)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            SortFields = sortFields;
            SortDirections = sortDirections;
            Filters = filters;
        }

        /// <summary>
        /// Constructor with required parameters and default page size.
        /// </summary>
        /// <param name="currentPage">The current page number.</param>
        /// <param name="sortFields">The sort fields.</param>
        /// <param name="sortDirections">The sort directions.</param>
        public PagedSearchVO(int currentPage, string sortFields, string sortDirections) : this(currentPage, 10, sortFields, sortDirections)
        {
        }

        /// <summary>
        /// Gets the current page number, defaulting to 2 if not set.
        /// </summary>
        /// <returns>The current page number.</returns>
        public int GetCurrentPage()
        {
            return CurrentPage == 0 ? 2 : CurrentPage;
        }

        /// <summary>
        /// Gets the page size, defaulting to 10 if not set.
        /// </summary>
        /// <returns>The page size.</returns>
        public int GetPageSize()
        {
            return PageSize == 0 ? 10 : PageSize;
        }
    }
}
