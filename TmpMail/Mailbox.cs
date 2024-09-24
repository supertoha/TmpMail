using System;
using TmpMail.Responses;

namespace TmpMail
{
    public class Mailbox
    {
        public Mailbox(TmpMail tmpMail, string email)
        {
            if (tmpMail == null) throw new ArgumentException("tmpMail");
            if (string.IsNullOrEmpty(email)) throw new ArgumentException("email");

            this._tmpMail = tmpMail;
            this._email = email;
        }

        private readonly TmpMail _tmpMail;
        private readonly string _email;
        private readonly IEnumerable<Email> _emails = new List<Email>();

        public string Email => this._email;
        internal HttpClient GetClient() => this._tmpMail.GetClient();

        public async Task<IEnumerable<Email>> GetEmailsAsync(bool newEmailsOnly=false)
        {
            var client = this.GetClient();
            var response = await client.GetAsync($"/get_letter_list?email={this._email}");
            if (response.IsSuccessStatusCode)
            {
                var litersListResponse = await response.Content.ReadAsAsync<BaseResponse<LettersListResponse>>();
                if (litersListResponse?.Ok == true)
                {
                    var result = new List<Email>();
                    foreach(var emailInfo in litersListResponse.Result.Emails)
                    {
                        var existEmail = this._emails.FirstOrDefault(x => x.Id == emailInfo.Id);
                        if(existEmail != null)
                        {
                            if(!newEmailsOnly)
                                result.Add(existEmail);
                            continue;
                        }

                        var emailContent = await this.GetEmailAsync(emailInfo.Id);
                        result.Add(emailContent);
                    }

                    return result;
                }
                else
                {
                    throw new TmpMailException($"Unable to get Emails, Error: {litersListResponse?.Error ?? "Unexpected failure"}");
                }
            }
            throw new TmpMailException($"Unable to get Emails");
        }

        public async Task<bool> DeleteAsync(bool throwException = true)
        {
            var client = this.GetClient();
            var response = await client.DeleteAsync($"/remove_email?email={this.Email}");
            if (response.IsSuccessStatusCode)
            {
                var deleteResponse = await response.Content.ReadAsAsync<BaseResponse<object>>();
                if (deleteResponse.Ok)
                    return true;

                if (throwException)
                    throw new TmpMailException($"Unable to delete mailbox. Error: {deleteResponse.Error}");
            }

            if (throwException)
                throw new TmpMailException($"Unable to delete mailbox. Wrong response code");

            return false;
        }

        private async Task<Email> GetEmailAsync(long letterId)
        {
            var client = this.GetClient();
            var response = await client.GetAsync($"/get_letter?letterId={letterId}");
            if (response.IsSuccessStatusCode)
            {
                var litersListResponse = await response.Content.ReadAsAsync<BaseResponse<Email>>();
                if(litersListResponse?.Ok == true)
                {
                    var letter = litersListResponse.Result;
                    letter.Id = letterId;
                    letter.Connect(this);

                    return letter;
                }
                else
                {
                    throw new TmpMailException($"Unable to get Email id:{letterId}, Error: {litersListResponse?.Error ?? "Unexpected response"}");
                }
            }

            throw new TmpMailException($"Unable to get Email");
        }
    }
}
