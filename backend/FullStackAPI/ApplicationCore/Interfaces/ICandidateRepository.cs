using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface ICandidateRepository
    {
        Task AddAsync(Candidate candidate);
        Task UpdateAsync(Candidate candidate);
        Task<Candidate> GetAsync(string id);
        Task<IEnumerable<Candidate>> GetAllAsync();
    }
}
