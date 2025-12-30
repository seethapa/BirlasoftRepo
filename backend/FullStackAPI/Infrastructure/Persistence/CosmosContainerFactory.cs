using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class CosmosContainerFactory
    {
        private readonly CosmosClient _client;
        private readonly CosmosDbSettings _settings;

        public CosmosContainerFactory(
            CosmosClient client,
            IOptions<CosmosDbSettings> options)
        {
            _client = client;
            _settings = options.Value;
        }

        public Container GetContainer(string containerKey)
        {
            var containerName = _settings.Containers[containerKey];
            return _client.GetContainer(
                _settings.DatabaseName,
                containerName);
        }
    }
}
