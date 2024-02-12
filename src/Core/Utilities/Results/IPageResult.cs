namespace Core.Utilities.Results
{
    public interface IPageResult<T> : IBaseResult
    {
        T Data { get; }

        int TotalPageCount { get; set; }
        int CurrentPage { get; set; }
        int TotalCount { get; set; }
        int PageSize { get; set; }
    }
}