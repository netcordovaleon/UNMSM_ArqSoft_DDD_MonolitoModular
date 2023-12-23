using Newtonsoft.Json;

namespace Gesfarma.Infrastructure.API;

public class ApiStringResponse
{
    [JsonProperty(PropertyName = "message")]
    public string Message { get; set; }

    public ApiStringResponse(string message)
    {
        Message = message;
    }
}