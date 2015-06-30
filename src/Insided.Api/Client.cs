using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Insided.Api
{
    public class Client
    {
        private readonly string _key;
        private readonly UriSignedConstructor _uriSignedConstructor;

        public Client(UriSignedConstructor uriSignedConstructor)
        {
            _uriSignedConstructor = uriSignedConstructor;

        }

        public async Task<JsonTextReader> Get(string url)
        {
            var now = DateTime.Now;

            var signedUri = _uriSignedConstructor.SignUri(url, now);

            var request = WebRequest.Create(signedUri) as HttpWebRequest;
            request.Accept = "application/json; version=1";

            ReplaceTheHeaderDateProperty(request,now);

            var response = await request.GetResponseAsync();
            return new JsonTextReader(new StreamReader(response.GetResponseStream()));

        }

        private void ReplaceTheHeaderDateProperty(WebRequest request,DateTime now)
        {
            MethodInfo priMethod = request.Headers.GetType().GetMethod("AddWithoutValidate", BindingFlags.Instance | BindingFlags.NonPublic);
            priMethod.Invoke(request.Headers, new[] { "Date", now.ToRfc2822Date() });
        }
    }
}