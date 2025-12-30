using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Infrastructure.Persistence;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repo
{
    public class ImpVoteRepository : IVoteRepository
    {
        private readonly Container _container;

        public ImpVoteRepository(CosmosContainerFactory factory)
        {
            _container = factory.GetContainer("Votes");
        }

        public async Task AddAsync(Vote vote)
        {
            await _container.CreateItemAsync(
                vote, new PartitionKey(vote.CandidateId));
        }

        public async Task<IEnumerable<Vote>> GetByCandidateIdAsync(string candidateId)
        {
            var query = new QueryDefinition(
                "SELECT * FROM c WHERE c.candidateId = @cid")
                .WithParameter("@cid", candidateId);

            var iterator = _container.GetItemQueryIterator<Vote>(query);
            var results = new List<Vote>();

            while (iterator.HasMoreResults)
            {
                var response = await iterator.ReadNextAsync();
                results.AddRange(response);
            }

            return results;
        }

        
    }
}
