using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Sevices
{
    public class ErrorMsg
    {

        public ErrorType Error { get; set; }
        public string Msg { get; set; }
    }

    public enum ErrorType
    {
        NotFound = 1,
        UnAuthorized = 2,
        NoContent = 3,
        Exception = 4
    }
}
