namespace Core.Utilities.Results
{
    public class PageResult<T>(T data, int totalPageCount, int currentPage, int totalCount, int pageSize) : IPageResult<T>
    {
        public T Data { get; set; } = data;
        public int TotalPageCount { get; set; } = totalPageCount;
        public int CurrentPage { get; set; } = currentPage;
        public int TotalCount { get; set; } = totalCount;
        public int PageSize { get; set; } = pageSize;

        public bool Success => true;
        public string Message => "";

        public int Code { get; set; } = 200;

        public bool Reload => false;
        public bool Done => false;
    }
}
