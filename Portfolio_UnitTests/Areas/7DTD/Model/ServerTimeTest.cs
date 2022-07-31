using Portfolio.Areas._7DTD.Models;

namespace Portfolio_UnitTests.Areas._7DTD.Model
{
    public class ServerTimeTest
    {

        [Theory]
        [MemberData("GetTestData")]
        public void Test(ServerTime time, int MinutesPassed, Expected expected)
        {
            time.Update(MinutesPassed);

            Assert.Equal(expected.Day, time.Day);
            Assert.Equal(expected.Hours, time.Hour);
        }

        private static IEnumerable<object[]> GetTestData()
        {
            yield return new object[] { new ServerTime(0, 0, 60), 60, new Expected(1, 0, 0) };
            yield return new object[] { new ServerTime(0, 0, 60), 30, new Expected(0, 12, 0) };
            yield return new object[] { new ServerTime(0, 12, 60), 90, new Expected(2, 0, 0) };
        }
    }
}
