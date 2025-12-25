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
    public class ImpCandidateRepository : ICandidateRepository
    {

        private readonly Container _container;

        public ImpCandidateRepository(CosmosContainerFactory factory)
        {
            _container = factory.GetContainer("Candidates");
        }
        public async Task AddAsync(Candidate candidate)
        {
            await _container.CreateItemAsync(
        candidate,
        new PartitionKey(candidate.Type)
    );
        }

        public async Task<Candidate> GetAsync(string id)
        {
            var response = await _container.ReadItemAsync<Candidate>(
                id,
                new PartitionKey(id) 
            );
            return response.Resource;
        }
      
        public async Task UpdateAsync(Candidate candidate)
        {
            await _container.ReplaceItemAsync(
                candidate,
                candidate.Id,
                new PartitionKey(candidate.Id)
            );
        }

        public async Task<IEnumerable<Candidate>> GetAllAsync()
        {
            var query = new QueryDefinition("SELECT * FROM c");
            var iterator = _container.GetItemQueryIterator<Candidate>(query);

            var candidates = new List<Candidate>();
            while (iterator.HasMoreResults)
            {
                var response = await iterator.ReadNextAsync();
                candidates.AddRange(response);
            }

            return candidates;
        }
    }
}
