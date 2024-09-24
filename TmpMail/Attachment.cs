using Newtonsoft.Json;
using System;

namespace TmpMail
{
    public class Attachment
    {
        internal Attachment() { }

        public string Filename { get; set; }

        [JsonProperty("MmimeType")]
        public string MimeType { get; set; }
        public int Size { get; set; }
        public long FileId { get; set; }

        private Email _email;

        internal void Connect(Email email)
        {
            this._email = email;
        }

        public async Task<byte[]> DownloadAsync()
        {
            if (this._email == null) throw new TmpMailException("Attachment is not connected");

            var client = this._email.GetClient();

            var response = await client.GetAsync($"/get_file?letterId={this._email.Id}&fileId={this.FileId}");
            if (!response.IsSuccessStatusCode)
                throw new TmpMailException("Unable to download attachment");

            return await response.Content.ReadAsByteArrayAsync();
        }
    }
}
