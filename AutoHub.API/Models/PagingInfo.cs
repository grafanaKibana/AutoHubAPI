namespace AutoHub.API.Models;

public record PagingInfo
{
    public string Prev { get; init; }

    public string Next { get; init; }
}