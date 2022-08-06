using Portfolio.Areas._7DTD.Models;

namespace Portfolio_UnitTests.Areas._7DTD.Model
{
    public class ViewResultTest
    {
        [Theory]
        [MemberData(nameof(GetTestData))]
        public void ViewReturnCorrectDaya(ServerTime time, Excepted expected)
        {
            var view = new BloodNightView(time);

            Assert.Equal(expected.BloodNightDay, view.BloodNightDay);

            Assert.Equal(expected.TimeBeforeBloodNight.Hours, view.TimeBeforeBloodNight.Hours);
            Assert.Equal(expected.TimeBeforeBloodNight.Minutes, view.TimeBeforeBloodNight.Minutes);
        }

        private static IEnumerable<object[]> GetTestData()
        {
            yield return new object[] { new ServerTime(1, 22, 60), new Excepted(7, TimeSpan.FromHours(6)) };
            yield return new object[] { new ServerTime(7, 0, 60), new Excepted(7, TimeSpan.FromMinutes(55)) };
            yield return new object[] { new ServerTime(1, 0, 60), new Excepted(7, new TimeSpan(6, 55, 0)) };
        }


        public class Excepted
        {
            public Excepted(int bloodNightDay, TimeSpan timeBeforeBloodNight)
            {
                BloodNightDay = bloodNightDay;
                TimeBeforeBloodNight = timeBeforeBloodNight;
            }

            public int BloodNightDay { get; set; }
            public TimeSpan TimeBeforeBloodNight { get; set; }
        }
    }

}
