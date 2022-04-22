namespace AutoHub.Domain.Enums;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Code Smell", "S2344:Enumeration type names should not have \"Flags\" or \"Enum\" suffixes", Justification = "<Pending>")]
public enum CarStatusEnum
{
    New = 1,
    OnHold = 2,
    ReadyForSale = 3,
    UnderRepair = 4,
    OnSale = 5,
    Sold = 6
}