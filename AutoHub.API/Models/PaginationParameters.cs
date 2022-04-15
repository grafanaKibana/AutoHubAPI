namespace AutoHub.API.Models;

public record PaginationParameters
{
    public int? Limit { get; init; }

    public string After { get; init; }
}
