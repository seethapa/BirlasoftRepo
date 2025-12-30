using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DTO
{
    public class CastVoteRequest
    {
        public string CandidateId { get; set; } = null!;
        public string VoterId { get; set; } = null!;
    }
}
