using NUnit.Framework;

namespace Insided.Api
{
    [TestFixture]
    public class SignatureHasherTests
    {
        [Test]
        public void Hash_WhenStringProvided_ShouldHashProperSignature()
        {
            
            //arrange
            var key = "helloworld";
            var sut = new SignatureHasher(key);

            //act
            var input = "Mon, 01 Jan 2015 12:00:00+0100\n\nhttps://api.almostinsided.com/thread?filter%5Bforumid%5D=123&order=title+DESC&pageSize=5\napplication/json; version=1";
            var actual = sut.Hash(input);

            //assert
            actual.Should(Be.EqualTo(@"UxOzeeCrGIcVSnnKdCeOEt1ulj5umyXoEpGMHFv/7Z0="));
        }
    }
}