using System;

namespace TmpMail
{
    public class TmpMailException : Exception
    {
        public TmpMailException(string message): base(message) { }
    }
}
