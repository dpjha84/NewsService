namespace NewsService.Paging
{
    public class PagingInfo
    {
        const int maxPageSize = 8;

        public int pageNumber { get; set; } = 1;

        public int _pageSize { get; set; } = 8;

        public int pageSize
        {

            get { return _pageSize; }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }
}
