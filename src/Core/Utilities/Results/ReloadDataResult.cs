namespace Core.Utilities.Results
{
    public class ReloadDataResult<T>(T data, string message) : DataResult<T>(200, data, true, true, message)
    {
    }
}
