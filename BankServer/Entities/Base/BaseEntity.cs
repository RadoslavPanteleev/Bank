using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BankServer.Entities.Base
{
    public class BaseEntity
    {
        public int Id { get; set; }

        [Timestamp]
        [JsonIgnore]
        public byte[]? Version { get; set; }
    }
}
