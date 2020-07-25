namespace NewsService.Paging
{
    public interface IPagedData
    {
        PagedList<T> GetPagedData<T>(PagingInfo pagingInfo);
    }
}
