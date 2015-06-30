using System;
using NUnit.Framework;

namespace Insided.Api
{
    [TestFixture]
    public class DateTimeExtentionsTests
    {
        [Test]
        public void ToRfc2822_WhenDateIsProvidedWithTimeZoneWithOffSet_ShouldEndUpWithProvidedTimeZoneInfoUtcOffset()
        {
            //arrange
            var sut = new DateTime(2015, 1, 1, 12, 0, 0);
            
            //act
            var actual = sut.ToRfc2822Date(TimeZoneInfo.CreateCustomTimeZone("Time+0200",new TimeSpan(2,0,0),"Time+0200","Time+0200") );

            //assert
            actual.Should(Be.StringEnding("+0200"));

        }

        [Test]
        public void ToRfc2822_WhenNoTimeZoneSpecified_ShouldEndUpWithLocalUtcOffset()
        {
            //arrange
            var sut = new DateTime(2015, 1, 1, 12, 0, 0);
            //act
            var actual = sut.ToRfc2822Date(TimeZoneInfo.Local);

            var hours = Math.Abs(TimeZoneInfo.Local.BaseUtcOffset.Hours).ToString("00");
            var minutes = Math.Abs(TimeZoneInfo.Local.BaseUtcOffset.Minutes).ToString("00");
            
            //assert
            actual.Should(Be.StringEnding("+"+hours+minutes));
        }

        [Test]
        public void ToRfc2822_WhenDateIsProvided_ShouldStartWithWeekDayInCultureInvariant()
        {
            //arrange
            var sut = new DateTime(2015, 1, 1, 12, 0, 0);
            
            //act
            var actual = sut.ToRfc2822Date();

            //assert
            actual.Should(Be.StringStarting("Thu"));
        }

        [Test]
        public void ToRfc2822_WhenDateIsProvidedWithTimeZone_ShouldReturnRfc2822Format()
        {
            //arrange
            var sut = new DateTime(2015, 1, 1, 12, 0, 0);

            //act
            var actual = sut.ToRfc2822Date(TimeZoneInfo.CreateCustomTimeZone("Time+0100",new TimeSpan(1,0,0),"Time+0100","Time+0100"));

            //assert
            actual.Should(Be.EqualTo("Thu, 01 Jan 2015 12:00:00+0100"));
        }


    }
}