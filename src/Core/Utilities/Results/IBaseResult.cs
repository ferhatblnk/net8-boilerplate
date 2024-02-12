namespace Core.Utilities.Results
{
    public interface IBaseResult
    {
        int Code { get; set; }
        bool Success { get; }
        bool Reload { get; }
        bool Done { get; }
        string Message { get; }
    }
}
