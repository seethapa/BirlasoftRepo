using ApplicationCore.DTO;
using ApplicationCore.Entities;
using Infrastructure.Persistence;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class CandidateService
    {
        private readonly Container _container;
        private readonly IConfiguration _configuration;

        public CandidateService(
            CosmosContainerFactory factory,
            IConfiguration configuration)
        {
            _container = factory.GetContainer("Candidates");
            _configuration = configuration;
        }


        public async Task AddAsync(CandidateRequest request)
        {
            
                var type = request.Type.Trim().ToLowerInvariant();

            //var existing = await GetByType(type);
            //if (existing != null)
            //    throw new InvalidOperationException("Candidate already exists");

            //var candidate = new Candidate
            //{
            //    candidate.Id = Guid.NewGuid().ToString(),
            //    Name = request.Name,
            //    Party = request.Party,
            //    VoteCount = request.VoteCount,
            //    Type = request.Type

            //};
            request.Id = Guid.NewGuid().ToString();

            await _container.CreateItemAsync(request, new PartitionKey(request.Party)
                );
            
        }

        // GET USER
        public async Task<Candidate?> GetById(string id)
        {
            var query = new QueryDefinition(
                "SELECT * FROM c WHERE c.id = @id")
                .WithParameter("@id", id);

            var iterator = _container.GetItemQueryIterator<Candidate>(query);
            var result = await iterator.ReadNextAsync();

            return result.FirstOrDefault();
        }

        public async Task<Candidate> GetAsync(string id)
        {
            var response = await _container.ReadItemAsync<Candidate>(
                id,
                new PartitionKey("candidate")
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

