using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class Vote
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }
        public string? CandidateId { get; set; }
        public string? VoterId { get; set; }
        public DateTime VotedAt { get; set; }
    }
}
