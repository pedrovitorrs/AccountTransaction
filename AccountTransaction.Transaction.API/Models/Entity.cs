using System.Security.Principal;
using System.Text.Json.Serialization;
using AccountTransaction.Transaction.API.Models.Interface;

namespace AccountTransaction.Transaction.API.Models
{
    public abstract class Entity : IEntity
    {
        public Entity() { }

        public Guid Id { get; set; }
    }
}
