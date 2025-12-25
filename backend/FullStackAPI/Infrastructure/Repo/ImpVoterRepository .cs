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
    public class ImpVoterRepository : IVoterRepository
    {
        private readonly Container _container;

        public ImpVoterRepository(CosmosContainerFactory factory)
        {
            _container = factory.GetContainer("Voters");
        }

        public async Task<Voter?> GetAsync(string voterId)
        {
            try
            {
                var response = await _container.ReadItemAsync<Voter>(
                    voterId,
                    new PartitionKey(voterId)
                );
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task AddAsync(Voter voter)
        {
            await _container.CreateItemAsync(
                voter,
                new PartitionKey(voter.Id)
            );
        }

        public async Task UpdateAsync(Voter voter)
        {
            await _container.ReplaceItemAsync(
                voter,
                voter.Id,
                new PartitionKey(voter.Id)
            );
        }

        public async Task<IEnumerable<Voter>> GetAllAsync()
        {
            var query = new QueryDefinition("SELECT * FROM c");
            var iterator = _container.GetItemQueryIterator<Voter>(query);

            var voters = new List<Voter>();
            while (iterator.HasMoreResults)
            {
                var response = await iterator.ReadNextAsync();
                voters.AddRange(response);
            }

            return voters;
        }
    }
}
