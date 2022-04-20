namespace AutoHub.API.Models;

public record PagingInfo
{
    public string First { get; init; }

    public string Last { get; init; }
}