namespace Portfolio.Areas._7DTD.Models
{
    public class BloodNightView
    {
        public ServerTime ServerTime { get; private set; }


        public int BloodNightDay { get; set; }

        public TimeSpan TimeLeft { get; set; }
        public DateTime StartTime { get; set; }

        public int DayOfWeek { get; set; }
        public int Percentage { get; set; }

        public BloodNightView(ServerTime ServerTime)
        {
            this.ServerTime = ServerTime;

            int dayPased = ServerTime.Day % 7;
            DayOfWeek = dayPased == 0 ? 7 : dayPased;
            BloodNightDay = DayOfWeek == 7 ? ServerTime.Day : ServerTime.Day + 7 - dayPased;

            TimeLeft = FindTimeBeforeBloodNight();
            StartTime = DateTime.Now.Add(TimeLeft);

            Percentage = FindPercentage();
        }


        private int FindMinsBeforeSunday()
        {
            int daysLeft = BloodNightDay - ServerTime.Day;

            return daysLeft * ServerTime.MinutesPerDay;
        }

        private int FindMinsBeforeNight()
        {
            int hoursLeft = 22 - ServerTime.Hour;

            return (int)(hoursLeft * ServerTime.MinutesPerHour);
        }

        private TimeSpan FindTimeBeforeBloodNight()
        {
            int minutesLeft = FindMinsBeforeSunday() + FindMinsBeforeNight();

            return TimeSpan.FromMinutes(minutesLeft);
        }

        // Only after FindTimeBeforeBloodNight
        private int FindPercentage()
        {
            const float percentPerDay = 1f / 7f;

            int daysPased = DayOfWeek - 1;
            float percent = daysPased * percentPerDay;

            if (ServerTime.Hour >= 22)
            {
                percent += percentPerDay;
            }
            else
            {
                percent += ServerTime.Hour / 22f * percentPerDay;
            }

            return (int)(percent * 100);
        }
    }
}
