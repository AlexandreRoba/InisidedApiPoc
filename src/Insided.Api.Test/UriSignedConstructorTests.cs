using System;
using System.Net;
using System.Web;
using NUnit.Framework;

namespace Insided.Api
{

    [TestFixture]
    public class UriSignedConstructorTests
    {
        private string _secretKey = "helloworld";
        private string _apiKey = "apikey";
        private string _uri = "https://api.almostinsided.com/thread?filter%5Bforumid%5D=123&order=title+DESC&pageSize=5";
        private DateTime _now = DateTime.Now;
        
        [Test]
        public void SignUri_WhenCalled_ShouldReturnUriWithTheApiKey()
        {
            //arrange
            var sut = new UriSignedConstructor(new SignatureConstructor(), new SignatureHasher(_secretKey), _apiKey);
            

            //act
            var actual = sut.SignUri(_uri, _now).ToString();

            //assert
            actual.Should(Be.StringContaining("apikey=" + _apiKey));
        }

        [Test]
        public void SignUri_WhenCalled_ShouldReturnUriWithSignature()
        {
            //arrange
            var hasher = new SignatureHasher(_secretKey);
            var signatureConstructor = new SignatureConstructor();

            var sut = new UriSignedConstructor(signatureConstructor, hasher, _apiKey);

            //act
            var actual = sut.SignUri(_uri, _now).ToString();

            //assert
            var sigValue = WebUtility.UrlEncode(hasher.Hash(signatureConstructor.BuildSignature(_uri, _now)));
            
            actual.Should(Be.StringContaining("sig=" + sigValue));

        }
    }
}