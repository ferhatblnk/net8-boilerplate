namespace Core.Utilities.Results
{
    public class ErrorResult : BaseResult
    {
        public ErrorResult(string message) : base(false, message)
        {
        }

        public ErrorResult(int code, string message) : base(code, false, message)
        {
        }

        public ErrorResult() : base(false)
        {
        }
    }
}
