namespace NewsService.Paging
{
    /// <summary>
    /// Interface for paged data
    /// </summary>
    public interface IPagedData
    {
        /// <summary>
        /// Get data in pages
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pagingInfo"></param>
        /// <returns></returns>
        PagedList<T> GetPagedData<T>(PagingInfo pagingInfo);
    }
}
