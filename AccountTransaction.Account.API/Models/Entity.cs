using System.Security.Principal;
using System.Text.Json.Serialization;

namespace AccountTransaction.Account.API.Models
{
    public abstract class Entity : IEntity
    {
        public Entity() { }

        [JsonIgnore]
        public int Id { get; set; }
    }
}
