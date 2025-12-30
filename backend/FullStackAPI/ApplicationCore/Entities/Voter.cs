using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class Voter
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public bool HasVoted { get; set; } = false;
    }
}
