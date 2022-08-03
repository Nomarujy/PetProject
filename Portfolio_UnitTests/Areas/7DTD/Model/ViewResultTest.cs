using Portfolio.Areas._7DTD.Models;

namespace Portfolio_UnitTests.Areas._7DTD.Model
{
    public class ViewResultTest
    {
        [Theory]
        [MemberData("GetTestData")]
        public void ViewReturnCorrectDaya(ServerTime time, Expected expected)
        {
            var view = new BloodNightView(time);

            Assert.Equal(expected.Day, view.BloodNightDay);
            Assert.Equal(expected.Hours, view.TimeToBloodNight.Hours);
            Assert.Equal(expected.Minutes, view.TimeToBloodNight.Minutes);
        }

        private static IEnumerable<object[]> GetTestData()
        {
            yield return new object[] { new ServerTime(1, 0, 60), new Expected(7, 6, 0) };
            yield return new object[] { new ServerTime(1, 12, 60), new Expected(7, 5, 30) };
            yield return new object[] { new ServerTime(7, 0, 60), new Expected(14, 7, 0) };
        }
    }

}
