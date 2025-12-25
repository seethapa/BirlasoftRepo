using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IVoteRepository
    {
       
            Task AddAsync(Vote vote);

            // Optional – for audit/reporting later
            Task<IEnumerable<Vote>> GetByCandidateIdAsync(string candidateId);
       
    }
}
