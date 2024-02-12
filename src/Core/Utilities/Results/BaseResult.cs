namespace Core.Utilities.Results
{
    public class BaseResult(bool success) : IBaseResult
    {
        public BaseResult(bool success, string message) : this(success)
        {
            Message = message;
        }

        public BaseResult(bool success, bool reload) : this(success)
        {
            Reload = reload;
        }

        public BaseResult(bool success, bool reload, string message) : this(success)
        {
            Message = message;
            Reload = reload;
        }

        public BaseResult(int code, bool success, string message) : this(success)
        {
            Code = code;
            Message = message;
        }

        public BaseResult(int code, bool success, bool reload, string message) : this(success)
        {
            Code = code;
            Message = message;
            Reload = reload;
        }

        public BaseResult(int code, bool success, bool reload, bool done, string message) : this(success)
        {
            Code = code;
            Message = message;
            Reload = reload;
            Done = done;
        }

        public bool Reload { get; }
        public bool Done { get; }
        public bool Success { get; } = success;
        public string Message { get; }
        public int Code { get; set; } = 200;
    }
}
