using AutoHub.Domain.Constants;

namespace AutoHub.BusinessLogic.Models;

public record PaginationParameters
{
    public PaginationParameters()
    {
        this.Limit = DefaultPaginationValues.DefaultLimit;
    }
    public PaginationParameters(int limit)
    {
        this.Limit = limit;
    }
    
    /// <example>100</example>
    public int? Limit { get; init; }

    public string Before { get; init; }

    public string After { get; init; }
}
