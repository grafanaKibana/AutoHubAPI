using System.Text.Json;

namespace AutoHub.API.Models
{
    public record ErrorDetails
    {
        public string Instance { get; init; }
        public string Message { get; init; }
        public string Details { get; init; }
        public string Type { get; init; }
        public int StatusCode { get; init; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}