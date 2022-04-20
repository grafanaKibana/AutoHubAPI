using System;
using System.Text;

namespace AutoHub.BusinessLogic.Common;

public static class Base64Helper
{
    public static string Encode(string text) => text != null ? Convert.ToBase64String(Encoding.UTF8.GetBytes(text)) : null;

    public static string Decode(string base64EncodedData) => base64EncodedData != null ? Encoding.UTF8.GetString(Convert.FromBase64String(base64EncodedData)) : null;

    public static bool TryDecode(string base64EncodedData, out string decodedString)
    {
        try
        {
            decodedString = Decode(base64EncodedData);
            return true;
        }
        catch (Exception)
        {
            decodedString = null;
            return false;
        }
    }
}
