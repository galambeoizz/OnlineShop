using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class ResponseModel
    {
        public ResCode Code { get; set; }
        public string Message { get; set; } 
    }

    public enum ResCode
    {
        Success = 0,
        Error = -1,
        Failure = 1
    }
        
}
