using System;
using System.Text;
using TmpMail.Responses;

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

        private Mailbox _mailBox;

        internal HttpClient GetClient() => this._mailBox.GetClient();

        internal void Connect(Mailbox mailBox)
        {
            this._mailBox = mailBox;

            if (this.Attachments?.Any() == true)
            {
                foreach (var attachment in this.Attachments)
                    attachment.Connect(this);
            }
        }

        public async Task<bool> DeleteAsync(bool throwException=true)
        {
            var client = this.GetClient();
            var response = await client.DeleteAsync($"/remove_letter?letterId={Id}");
            if (response.IsSuccessStatusCode)
            {
                var deleteResponse = await response.Content.ReadAsAsync<BaseResponse<object>>();
                if (deleteResponse.Ok)
                    return true;

                if(throwException)
                    throw new TmpMailException($"Unable to delete email. Error: {deleteResponse.Error}");
            }

            if(throwException)
                throw new TmpMailException($"Unable to delete email. Wrong response code");

            return false;
        }

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
