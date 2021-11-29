using System.Text.Json;

namespace AutoHub.API.Models
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Instance { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}