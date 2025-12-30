using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ApplicationCore.Entities
{
    public class Usermodel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("email")]   
        public string Email { get; set; } 

        [JsonProperty("passwordHash")]
        public string PasswordHash { get; set; } = null!;

        [JsonProperty("FirstName")]
        public string FirstName { get; set; } = null!;

        [JsonProperty("LastName")]
        public string LastName { get; set; } = null!;

        [JsonProperty("role")]
        public string Role { get; set; } = null!;

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    }
}
