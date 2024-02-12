using System.Collections.Generic;

namespace Core.CrossCuttingConcerns.Logging
{
    public class LogDetail
    {
        public string TargetName { get; set; }
        public string MethodName { get; set; }
        public List<LogParameter> LogParameters { get; set; }        
    }
}
