using System;
using System.Text;

namespace TmpMail
{
    internal static class Base64Helper
    {
        internal static string StringToBase64String(string originalString)
        {
            try
            {
                return Convert.ToBase64String(Encoding.UTF8.GetBytes(originalString));
            }
            catch (Exception exc)
            {
                throw new TmpMailException($"Unable to convert text to BASE64 string. Exception: {exc.Message}");
            }
        }
    }
}
