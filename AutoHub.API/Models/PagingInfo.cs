using AutoHub.BusinessLogic.Common;

namespace AutoHub.API.Models;

public record PagingInfo
{
    public PagingInfo(int firstId, int lastId)
    {
        First = Base64Helper.Encode(firstId.ToString());
        Last = Base64Helper.Encode(lastId.ToString());
    }

    public string First { get; init; }

    public string Last { get; init; }
}