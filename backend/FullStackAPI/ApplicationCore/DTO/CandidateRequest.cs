using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApplicationCore.DTO
{
    public class CandidateRequest
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = null!;

        

        [JsonPropertyName("type")]
        [Required]
        public string Type { get; set; } = "candidate";

        public string Name { get; set; } = null!;
        public string Party { get; set; } = null!;
        public int VoteCount { get; set; }
    }
}
