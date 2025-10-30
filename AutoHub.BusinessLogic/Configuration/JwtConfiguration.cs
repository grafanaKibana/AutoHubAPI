namespace AutoHub.BusinessLogic.Configuration;

public record JwtConfiguration
{
    public string Key { get; init; }
    public int HoursToExpire { get; init; }
    public string Issuer { get; init; }
    public string Audience { get; init; }
}
