namespace Core.Utilities.Results
{
    public class DataResult<T> : BaseResult, IDataResult<T>
    {
        public DataResult(T data, bool success, string message) : base(success, message)
        {
            Data = data;
        }

        public DataResult(int code, T data, bool success, string message) : base(code, success, message)
        {
            Data = data;
        }

        public DataResult(int code, T data, bool success, bool reload, string message) : base(code, success, reload, message)
        {
            Data = data;
        }

        public DataResult(T data, bool success) : base(success)
        {
            Data = data;
        }

        public T Data { get; set; }
    }
}
