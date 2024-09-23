using System;
using TmpMail.Responses;

namespace TmpMail
{
    public class Mailbox
    {
        internal Mailbox(TmpMail tmpMail, string email)
        {
            this._tmpMail = tmpMail;
            this._email = email;
        }

        private readonly TmpMail _tmpMail;
        private readonly string _email;
        private readonly IEnumerable<Email> _emails = new List<Email>();

        public async Task<IEnumerable<Email>> GetEmailsAsync(bool newEmailsOnly=false)
        {
            var client = this._tmpMail.GetClient();
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

        private async Task<Email> GetEmailAsync(long letterId)
        {
            var client = this._tmpMail.GetClient();
            var response = await client.GetAsync($"/get_letter?letterId={letterId}");
            if (response.IsSuccessStatusCode)
            {
                var litersListResponse = await response.Content.ReadAsAsync<BaseResponse<Email>>();
                if(litersListResponse?.Ok == true)
                {
                    litersListResponse.Result.Id = letterId;
                    return litersListResponse.Result;
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
