namespace Core.Utilities.Results
{
    public class ReloadResult : BaseResult
    {
        public ReloadResult(string message) : base(true, true, message)
        {
        }

        public ReloadResult() : base(true, true)
        {
        }
    }
}
