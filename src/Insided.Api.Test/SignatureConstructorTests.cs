using System;
using NUnit.Framework;

namespace Insided.Api
{
    [TestFixture]
    public class SignatureConstructorTests
    {

        private TimeZoneInfo timeZoneInfo = TimeZoneInfo.CreateCustomTimeZone("TimeZone+0100", new TimeSpan(1, 0, 0), "TimeZone+0100", "TimeZone+0100");
        private DateTime now = new DateTime(2015, 1, 1, 12, 0, 0);
        private string url = "https://api.almostinsided.com/thread?filter%5Bforumid%5D=123&order=title+DESC&pageSize=5";
        [Test]
        public void BuildSignature_WhenCalled_ShouldStartWithRfc2822Date()
        {
            //arrange
            var sut = new SignatureConstructor();

            //act
            var actual = sut.BuildSignature(url, now);

            //assert
            actual.Should(Be.StringStarting("Thu, 01 Jan 2015 12:00:00+0000"));
        }

        [Test]
        public void BuildSignature_WhenCalled_ShouldEndWithApplicationType()
        {
            //arrange
            var sut = new SignatureConstructor();

            //act
            var actual = sut.BuildSignature(url, now);


            //assert
            actual.Should(Be.StringEnding("application/json; version=1"));
        }

        [Test]
        public void BuildSignature_WhenCalled_ShouldHaveTwoCarriageReturnsAfterTheRfc2822Date()
        {
            //arrange
            var sut = new SignatureConstructor();
            
            //act
            var actual = sut.BuildSignature(url, now);
            

            //assert
            var subString = actual.Substring(now.ToRfc2822Date(timeZoneInfo).Length);
            subString.Should(Be.StringStarting("\n\n"));

        }

        [Test]
        public void BuildSignature_WhenCalled_ShouldHaveTheUriAfterTheRfc2822DateAndTwoCarriageReturns()
        {
            //arrange
            var sut = new SignatureConstructor();

            //act
            var actual = sut.BuildSignature(url, now);


            //assert
            var subString = actual.Substring(now.ToRfc2822Date(timeZoneInfo).Length);
            subString = subString.Substring("\n\n".Length);
            subString.Should(Be.StringStarting(url.ToString()));

        }

        [Test]
        public void BuildSignature_WhenCalled_ShouldHaveACariageReturnAfterTheUri()
        {
            //arrange
            var sut = new SignatureConstructor();

            //act
            var actual = sut.BuildSignature(url, now);

            //assert
            var subString = actual.Substring(now.ToRfc2822Date(timeZoneInfo).Length);
            subString = subString.Substring("\n\n".Length);
            subString = subString.Substring(url.ToString().Length);

            subString.Should(Be.StringStarting("\n"));

        }

        
    }
}