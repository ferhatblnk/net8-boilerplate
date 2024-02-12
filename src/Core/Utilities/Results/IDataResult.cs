namespace Core.Utilities.Results
{
    public interface IDataResult<out T> : IBaseResult
    {
        T Data { get; }
    }
}
