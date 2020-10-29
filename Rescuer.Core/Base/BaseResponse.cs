using System.Collections.Generic;

namespace Rescuer.Core.Base
{
    public class BaseResponse<T>
        where T : class
    {
        public BaseResponse()
        {

        }
        public T Result { get; set; }
        public string Message { get; set; }
        public List<string> ValidationMessage { get; set; }
        public int StatusCode { get; set; }
        public bool Success { get; set; }
        public bool IsError { get; set; }
    }
}