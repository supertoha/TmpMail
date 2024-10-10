using System;
using System.Text.Json.Serialization;

namespace TmpMail.Requests
{
    internal class CreateEmailRequest
    {
        [JsonPropertyName("html")]
        public string Html { get; set; }

        [JsonPropertyName("subject")]
        public string Subject { get; set; }

        [JsonPropertyName("to")]
        public string To { get; set; }

        [JsonPropertyName("from")]
        public string From { get; set; }
    }
}
