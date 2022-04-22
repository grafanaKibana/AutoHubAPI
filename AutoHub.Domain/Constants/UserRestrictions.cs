namespace AutoHub.Domain.Constants;

public static class UserRestrictions
{
    public const int MinPasswordLength = 10;

    public const string UniversalPhoneNumberRegex = @"((?:\+|00)[17](?: |\-)?|(?:\+|00)[1-9]\d{0,2}(?: |\-)?|(?:\+|00)1\-\d{3}(?: |\-)?)?(0\d|\([0-9]{3}\)|[1-9]{0,3})(?:((?: |\-)[0-9]{2}){4}|((?:[0-9]{2}){4})|((?: |\-)[0-9]{3}(?: |\-)[0-9]{4})|([0-9]{7}))";

    public const int MaxFailedAccessAttempts = 5;

    public const int LockoutDurationInMinutes = 3;
}