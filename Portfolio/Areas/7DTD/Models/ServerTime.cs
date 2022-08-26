namespace Portfolio.Areas._7DTD.Models
{
    public class ServerTime
    {
        public int Day { get; private set; }
        public int Hour { get; private set; }
        public int MinutesPerDay { get; private set; }

        public float MinutesPerHour { get; private set; }

        public ServerTime()
        {
            Day = 1;
            Hour = 0;
            MinutesPerDay = 60;
            MinutesPerHour = MinutesPerDay / 24f;
        }

        public ServerTime(int Day, int Hour, int MinutesPerDay)
        {
            this.Day = Day;
            this.Hour = Hour % 24; //Validate Hours
            this.MinutesPerDay = MinutesPerDay;
            MinutesPerHour = MinutesPerDay / 24f;
        }

        private float Correction = 0f;

        public void Update(int realMinutesPased)
        {
            Correction += realMinutesPased;

            int daysPased = (int)(Correction / MinutesPerDay);
            Correction -= daysPased * MinutesPerDay;

            int hourPased = (int)(Correction / MinutesPerHour);
            Correction -= hourPased * MinutesPerHour;

            Day += daysPased;
            Hour += hourPased;

            if (Hour >= 24)
            {
                daysPased = Hour / 24;
                Day += daysPased;
                Hour %= 24;
            }
        }
    }
}
