using System;

namespace TmpMail.Responses
{
    internal class LettersListResponse
    {
        public IEnumerable<EmailInfo> Emails { get; set; }
    }
}
