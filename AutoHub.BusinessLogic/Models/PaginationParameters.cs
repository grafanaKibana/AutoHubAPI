namespace AutoHub.BusinessLogic.Models;

public record PaginationParameters
{
    public int? Limit { get; init; }

    public string Before { get; init; }

    public string After { get; init; }
}
