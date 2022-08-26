using Portfolio.Areas._7DTD.Models;

namespace Portfolio_UnitTests.Areas._7DTD.Models
{
    public class ServerTimeTest
    {
        private readonly ServerTime _serverTime;

        public ServerTimeTest()
        {
            _serverTime = new(1, 0, 60);
        }

        [Theory, MemberData(nameof(GetData))]
        public void Update(int minutesPassed, Expected expected)
        {
            _serverTime.Update(minutesPassed);

            Assert.Equal(expected.Day, _serverTime.Day);
            Assert.Equal(expected.Hour, _serverTime.Hour);
        }

        public static IEnumerable<object[]> GetData()
        {
            yield return new object[] { 0, new Expected(1, 0) };
            yield return new object[] { 30, new Expected(1, 12) };
            yield return new object[] { 55, new Expected(1, 22) };
            yield return new object[] { 60, new Expected(2, 0) };
            yield return new object[] { 360, new Expected(7, 0) };
        }
    }

    public class Expected
    {
        public Expected(int day, int hour)
        {
            Day = day;
            Hour = hour;
        }

        public int Day { get; set; }
        public int Hour { get; set; }
    }
}
