using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class CosmosDbSettings
    {
        public string Account { get; set; } = null!;
        public string Key { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public Dictionary<string, string> Containers { get; set; } = new();
    }
}
