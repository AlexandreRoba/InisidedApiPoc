using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Insided.Api
{
    public class UriSignedConstructor
    {
        private readonly SignatureConstructor _signatureConstructor;
        private readonly SignatureHasher _signatureHasher;
        private readonly string _apiKey;
        
        public UriSignedConstructor(SignatureConstructor signatureConstructor,SignatureHasher signatureHasher,string apiKey)
        {
            _apiKey = apiKey;
            _signatureConstructor = signatureConstructor;
            _signatureHasher = signatureHasher;
        }


        public string SignUri(string url, DateTime now)
        {

            url = AddQuestionMarkAtTheEndIfNoQuerystring(url);

            var sig = _signatureConstructor.BuildSignature(url, now);
            var hash = _signatureHasher.Hash(sig);

            var queryStringDictionary = HttpUtility.ParseQueryString(GetQueryString(url));
            queryStringDictionary.Add("apikey",_apiKey);
            queryStringDictionary.Add("sig", hash);

            var queryStringWithSignature = ToQueryString(queryStringDictionary);

            queryStringWithSignature = AddLeadingAmpercentWhenThefirstParameterIsApiKey(queryStringWithSignature);

            var returnUri = string.Format("{0}{1}", GetAbsolutePath(url), queryStringWithSignature);

            returnUri = CorrectUrlEncodingWithUpperCaseAfterThepercentage(returnUri);

            Debug.WriteLine("SignedUri------------------------------------");
            Debug.WriteLine(returnUri);
            Debug.WriteLine("---------------------------------------------");
            return returnUri;
        }

        public static string CorrectUrlEncodingWithUpperCaseAfterThepercentage(string s)
        {
            char[] temp = s.ToCharArray();
            for (int i = 0; i < temp.Length - 2; i++)
            {
                if (temp[i] == '%')
                {
                    temp[i + 1] = char.ToUpper(temp[i + 1]);
                    temp[i + 2] = char.ToUpper(temp[i + 2]);
                }
            }
            return new string(temp);
        }



        private string AddLeadingAmpercentWhenThefirstParameterIsApiKey(string queryStringWithSignature)
        {
            if (queryStringWithSignature.StartsWith("?apikey"))
                queryStringWithSignature = "?&" + queryStringWithSignature.Substring(1);
            return queryStringWithSignature;
        }

        private string AddQuestionMarkAtTheEndIfNoQuerystring(string url)
        {
            if (url.Contains("?"))
                return url;
            return url + "?";
        }

        private string GetAbsolutePath(string url)
        {
            if (url.Contains("?"))
                return url.Substring(0,url.IndexOf("?"));
            return url;
        }

        private string GetQueryString(string url)
        {
            if (url.Contains("?"))
                return url.Substring(url.IndexOf("?")+1);
            return url;
        }

        private string ToQueryString(NameValueCollection nvc)
        {
            var array = (from key in nvc.AllKeys
                         from value in nvc.GetValues(key)
                         select string.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(value)))
                .ToArray();
            return "?" + string.Join("&", array);
        }
    }
}