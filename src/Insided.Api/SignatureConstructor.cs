using System;
using System.Diagnostics;
using System.Text;

namespace Insided.Api
{
    public class SignatureConstructor
    {

        public SignatureConstructor()
        {

        }

        public string BuildSignature(string uri, DateTime now)
        {
            var stb = new StringBuilder();
            stb.Append(now.ToRfc2822Date(TimeZoneInfo.Local));
            stb.Append("\n");
            stb.Append("\n");
            stb.Append(uri);
            stb.Append("\n");
            stb.Append("application/json; version=1");
            var result = stb.ToString();
            Debug.WriteLine("Begin of Signature------------------------------------------");
            Debug.WriteLine(result);
            Debug.WriteLine("------------------------------------------------------------");
            return result;
        }
        
    }
}