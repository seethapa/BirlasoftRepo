using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class Candidate
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;
        [JsonPropertyName("party")]
        public string Party { get; set; } = null!;
        [JsonPropertyName("voteCount")]
        public int VoteCount { get; set; }

        // MUST exist and match PK
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
