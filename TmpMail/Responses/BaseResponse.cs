using System;

namespace TmpMail.Responses
{
    internal class BaseResponse<T> where T : class
    {
        public string Error { get; set; }
        public bool Ok { get; set; }
        public T Result { get; set; }
    }
}
