namespace Portfolio.Areas._7DTD.Models
{
    public class BloodNightView
    {
        public ServerTime ServerTime { get; private set; }
        public int BloodNightDay { get; private set; }
        public TimeSpan TimeBeforeBloodNight { get; private set; }

        public BloodNightView(ServerTime ServerTime)
        {
            this.ServerTime = ServerTime;
            TimeBeforeBloodNight = new();

            int dayOfWeek = ServerTime.Day % 7;
            int daysLeft = dayOfWeek == 0 ? 0 : 7 - dayOfWeek;

            BloodNightDay = ServerTime.Day + daysLeft;

            CalculateTimeToBloodNight(daysLeft);
        }


        private void CalculateTimeToBloodNight(int daysLeft)
        {
            int minsBeforeRedDay = daysLeft * ServerTime.MinutesPerDay;
            TimeBeforeBloodNight = TimeBeforeBloodNight.Add(TimeSpan.FromMinutes(minsBeforeRedDay));

            int HoursLeft = 22 - ServerTime.Hour;
            int minsBeforeNight = (int)(HoursLeft * ServerTime.MinutesPerHour);

            TimeBeforeBloodNight = TimeBeforeBloodNight.Add(TimeSpan.FromMinutes(minsBeforeNight));
        }
    }
}
