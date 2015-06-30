using NUnit.Framework;

namespace Insided.Api
{
    [TestFixture]
    public class ClientTests
    {
        private string _secretKey = "xxxxxxxxxxx";
        private string _apiKey = "xxxxxxxxx";
        private string _url = "https://api.insided.com/thread";
        [Test]
        public void Get_WhenCalled_ShouldMAkeThecallToInsided()
        {
            //arrange
            
            var sut = new Client(new UriSignedConstructor(new SignatureConstructor(), new SignatureHasher(_secretKey),_apiKey ));

            //act
            var actual = sut.Get(_url);
            var result = actual.Result;

            //assert
            Assert.IsNotNull(result);
        }
    }
}