using RestfullWithAspNet.Model.Base;

namespace RestfullWithAspNet.Repository
{
    /// <summary>
    /// Interface for the generic repository.
    /// </summary>
    /// <typeparam name="T">The type of entity.</typeparam>
    public interface IRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// Creates a new item.
        /// </summary>
        /// <param name="item">The item to be created.</param>
        /// <returns>The created item.</returns>
        T Create(T item);

        /// <summary>
        /// Finds an item by its ID.
        /// </summary>
        /// <param name="id">The ID of the item.</param>
        /// <returns>The item with the specified ID.</returns>
        T FindById(long id);

        /// <summary>
        /// Finds all items.
        /// </summary>
        /// <returns>A list of items.</returns>
        List<T> FindAll();

        /// <summary>
        /// Updates an item.
        /// </summary>
        /// <param name="item">The item to be updated.</param>
        /// <returns>The updated item.</returns>
        T Update(T item);

        /// <summary>
        /// Deletes an item by its ID.
        /// </summary>
        /// <param name="id">The ID of the item to be deleted.</param>
        void Delete(long id);

        /// <summary>
        /// Checks if an item exists in the database by its ID.
        /// </summary>
        /// <param name="id">The ID of the item to check.</param>
        /// <returns>True if the item exists, false otherwise.</returns>
        bool Exists(long id);

        /// <summary>
        /// Finds items with a paged search.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        List<T> FindWithPagedSearch(string query);

        /// <summary>
        /// Gets the count of items with a paged search.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        int GetCount(string query);
    }
}
