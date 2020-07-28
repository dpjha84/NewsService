namespace NewsService.Paging
{
    /// <summary>
    /// Paging info that client passes
    /// </summary>
    public class PagingInfo
    {
        public const int maxPageSize = 8;
        public const int defaultPageSize = 8;
        public const int defaultPageNumber = 1;
        private int _pageSize = defaultPageSize;
        private int _pageNumber = defaultPageNumber;


        public int pageNumber
        {
            get => _pageNumber;
            set
            {
                if (value < 1) _pageNumber = defaultPageNumber;
                else _pageNumber = value;
            }
        }

        public int pageSize
        {
            get => _pageSize;
            set
            {
                if (value < 1) _pageSize = defaultPageSize;
                else if (value > maxPageSize) _pageSize = maxPageSize;
                else _pageSize = value;
            }
        }
    }
}
