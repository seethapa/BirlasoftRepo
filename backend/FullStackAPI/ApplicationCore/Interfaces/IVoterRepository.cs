using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IVoterRepository
    {
        Task<Voter> GetAsync(string id);
        Task AddAsync(Voter voter);
        Task UpdateAsync(Voter voter);
        Task<IEnumerable<Voter>> GetAllAsync();
    }
}
