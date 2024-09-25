using System.Net.Http.Headers;
using TmpMail.Responses;

namespace TmpMail
{
    public class TmpMail : IDisposable
    {
        /// <summary>
        /// Create TmpMail instance 
        /// </summary>
        /// <param name="apiKey">API KEY. You can get API kay at https://rapidapi.com/supertoha/api/temporary-email-service/</param>
        public TmpMail(string apiKey)
        {
            this._apiKey = apiKey;
        }

        private readonly string _apiKey;
        private HttpClient _client;

        internal HttpClient GetClient()
        {
            if(this._client == null)
            {
                this.ValidateService();

                this._client = new HttpClient();
                this._client.BaseAddress = new Uri("https://temporary-email-service.p.rapidapi.com/");
                this._client.DefaultRequestHeaders.Add("X-RapidAPI-Key", this._apiKey);
                this._client.DefaultRequestHeaders.Add("X-RapidAPI-Host", "temporary-email-service.p.rapidapi.com");
                this._client.DefaultRequestHeaders.Accept.Clear();
                this._client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }

            return this._client;
        }

        private void ValidateService()
        {
            if (string.IsNullOrEmpty(this._apiKey) || this._apiKey.Equals("<YOUR API KEY>", StringComparison.CurrentCultureIgnoreCase))
                throw new TmpMailException("Invalid API Key. Get API Key: https://rapidapi.com/supertoha/api/temporary-email-service/");
        }

        /// <summary>
        /// Create Mailbox (email address)
        /// </summary>
        /// <returns>Mailbox instance</returns>
        /// <exception cref="TmpMailException"></exception>
        public async Task<Mailbox> CreateMailboxAsync()
        {
            var client = this.GetClient();

            var content = new StringContent("{}")
            {
                Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
            };
            var response = await client.PostAsync("create_email", content);
            if (response.IsSuccessStatusCode)
            {
                var mailboxResponse = await response.Content.ReadAsAsync<BaseResponse<CreateMailboxResponse>>();
                if (mailboxResponse?.Ok == true)
                {
                    return new Mailbox(this, mailboxResponse.Result.Email);
                }
                else
                {
                    throw new TmpMailException($"Unable to create mailbox: {mailboxResponse?.Error ?? "Unexpected response"}");
                }
            }

            throw new TmpMailException($"Unable to create mailbox");
        }


        public void Dispose()
        {
            this._client?.Dispose();
        }
    }
}
