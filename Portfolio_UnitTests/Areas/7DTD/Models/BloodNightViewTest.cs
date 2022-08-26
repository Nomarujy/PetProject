using Portfolio.Areas._7DTD.Models;

namespace Portfolio_UnitTests.Areas._7DTD.Models
{
    public class BloodNightViewTest
    {
        [Fact]
        public void ViewInitialize()
        {
            BloodNightView view = new(new ServerTime(1, 0, 60));

            var startTime = view.StartTime;

            Assert.Equal(7, view.BloodNightDay);
            Assert.Equal(1, view.DayOfWeek);

            Assert.True(DateTime.Now < startTime);
        }

        [Theory, MemberData(nameof(GetTimeLeftData))]
        public void CheckTimeLeft(ServerTime serverTime, TimeSpan timeLeft)
        {
            BloodNightView view = new(serverTime);

            Assert.Equal(timeLeft, view.TimeLeft);
        }

        public static IEnumerable<object[]> GetTimeLeftData()
        {
            yield return new object[] { new ServerTime(1, 22, 60), TimeSpan.FromHours(6) };
            yield return new object[] { new ServerTime(7, 0, 60), TimeSpan.FromMinutes(55) };
        }

        [Theory]
        [MemberData(nameof(GetPercentageData))]
        public void CheckPercentage(ServerTime serverTime, int expectedPercentage)
        {
            BloodNightView view = new(serverTime);

            Assert.Equal(expectedPercentage, view.Percentage);
        }


        public static IEnumerable<object[]> GetPercentageData()
        {
            yield return new object[] { new ServerTime(1, 0, 60), 0 };
            yield return new object[] { new ServerTime(4, 11, 60), 50 };
            yield return new object[] { new ServerTime(7, 22, 60), 100 };
        }

    }
}
