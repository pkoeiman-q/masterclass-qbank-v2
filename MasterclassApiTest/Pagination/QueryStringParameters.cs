namespace MasterclassApiTest.Pagination
{
    public abstract class QueryStringParameters
    {
        // How many results the page can show at most
        const int maxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                // Cap the _pageSize at maxPageSize
                _pageSize = value > maxPageSize ? maxPageSize : value;
            }
        }
    }
}
