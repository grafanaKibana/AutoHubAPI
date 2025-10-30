using System.Text.Json;

namespace AutoHub.API.Models
{
    public record ErrorDetails(string Instance, string Message, string Details, string Type, string StackTrace, int StatusCode)
    {
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}