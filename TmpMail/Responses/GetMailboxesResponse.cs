using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TmpMail.Responses
{
    internal class GetMailboxesResponse
    {
        public MailboxInfo[] Mailboxes { get; set; }
    }
}
