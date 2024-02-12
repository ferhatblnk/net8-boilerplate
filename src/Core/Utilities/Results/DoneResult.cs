namespace Core.Utilities.Results
{
    public class DoneResult : BaseResult
    {
        public DoneResult(string message) : base(200, true, true, true, message)
        {
        }
    }
}
