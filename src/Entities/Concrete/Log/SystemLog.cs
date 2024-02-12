using Core.Entities.Concrete;
using System;

namespace Entities.Concrete
{
    public class TSystemLog : LogEntity
    {
        public string Endpoint { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
        public string Detail { get; set; }
    }
}
