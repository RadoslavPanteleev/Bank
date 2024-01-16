using Swashbuckle.AspNetCore.Annotations;

namespace BankServer.Models.Base
{
    public class BaseModel
    {
        [SwaggerSchema(Description = "Field for internal use only. Pass it with 0 only when creating new record.", Nullable = true, Format = null)]
        public int UpdateCounter { get; set; } = 0;
    }
}
