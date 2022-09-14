using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Discarding
{
    [JsonObject]
    internal class Message
    {
        [JsonPropertyName("messages")]
        public List<Message> Messages { get; set; }


        [JsonPropertyName("date")]
        public string Date { get; set; }


        [JsonPropertyName("from")]
        public string From { get; set; }


        [JsonPropertyName("text")]
        public string Text { get; set; }
    }
}
