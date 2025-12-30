using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Infrastructure.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{

    public class VotingService
    {
        //private readonly ICandidateRepository _candidateRepo;
        private readonly IVoterRepository _voterRepo;
        private readonly IVoteRepository _voteRepo;

        public VotingService(
            //ICandidateRepository candidateRepo,
            IVoterRepository voterRepo,
            IVoteRepository voteRepo)
        {
            //_candidateRepo = candidateRepo;
            _voterRepo = voterRepo;
            _voteRepo = voteRepo;
        }

        public async Task CastVote(string candidateId, string voterId)
        {
            var voter = await _voterRepo.GetAsync(voterId);

            //if (voter == null)
            //    throw new KeyNotFoundException("Voter not found");

            if (voter.HasVoted)
                throw new InvalidOperationException("Voter already voted");

            //var candidate = await _candidateRepo.GetAsync(candidateId);

            //if (candidate == null)
            //    throw new KeyNotFoundException("Candidate not found");

            //candidate.VoteCount++;
            //voter.HasVoted = true;

            //await _candidateRepo.UpdateAsync(candidate);
            //await _voterRepo.UpdateAsync(voter);

            await _voteRepo.AddAsync(new Vote
            {
                Id = Guid.NewGuid().ToString(),
                CandidateId = candidateId,
                VoterId = voterId,
                VotedAt = DateTime.UtcNow
            });
        }



    }
}


