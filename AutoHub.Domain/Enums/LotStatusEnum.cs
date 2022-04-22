namespace AutoHub.Domain.Enums;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Code Smell", "S2344:Enumeration type names should not have \"Flags\" or \"Enum\" suffixes", Justification = "<Pending>")]
public enum LotStatusEnum
{
    New = 1,
    NotStarted = 2,
    InProgress = 3,
    EndedUp = 4
}