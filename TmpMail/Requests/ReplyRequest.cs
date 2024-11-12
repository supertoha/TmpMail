using System;
using System.Text.Json.Serialization;

namespace TmpMail.Requests
{
    internal class ReplyRequest
    {
        [JsonPropertyName("html")]
        public string Html { get; set; }

        [JsonPropertyName("letterId")]
        public long LetterId { get; set; }
    }
}
