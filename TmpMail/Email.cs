using System;
using System.Text;

namespace TmpMail
{
    public class Email
    {
        internal Email()
        {
        }

        internal long Id { get; set; }

        public string Html { get; set; }

        public EmailAddress From { get; set; }
        public EmailAddress[] To { get; set; }
        public string Subject { get; set; }
        public DateTime Date { get; set; }
        public string MessageId { get; set; }
        public Attachment[] Attachments { get; set; }
        public bool CanReply { get; set; }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"FROM: {this.From}");
            stringBuilder.AppendLine($"TO: {string.Join(",", this.To.ToList())}");
            stringBuilder.AppendLine($"SUBJECT: {Subject}");
            stringBuilder.AppendLine($"MESSAGE: {Html}");

            return stringBuilder.ToString();
        }
    }
}
