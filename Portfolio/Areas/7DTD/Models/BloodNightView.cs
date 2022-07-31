namespace Portfolio.Areas._7DTD.Models
{
    public class BloodNightView
    {
        public ServerTime ServerTime { get; private set; }
        public int BloodNightDay { get; private set; }
        public TimeSpan TimeToBloodNight { get; private set; }

        public BloodNightView(ServerTime ServerTime)
        {
            this.ServerTime = ServerTime;

            int daysBeforeNight = 7 - ServerTime.Day % 7;

            BloodNightDay = ServerTime.Day + daysBeforeNight;

            CalculateTimeToBloodNight(daysBeforeNight);
        }


        private void CalculateTimeToBloodNight(int daysBeforeNight)
        {
            float currentDayTimePassed = ServerTime.Hour / 24f * ServerTime.MinutesPerDay;

            int minutesBeforeNightStart = ServerTime.MinutesPerDay * daysBeforeNight - (int)currentDayTimePassed;

            TimeToBloodNight = new TimeSpan(minutesBeforeNightStart / 60, minutesBeforeNightStart % 60, 0);
        }
    }
}
