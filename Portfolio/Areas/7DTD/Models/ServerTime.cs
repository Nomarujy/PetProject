namespace Portfolio.Areas._7DTD.Models
{
    public class ServerTime
    {
        public int Day { get; private set; }
        public int Hour { get; private set; }
        public int MinutesPerDay { get; private set; }

        public ServerTime()
        {
            Day = 0;
            Hour = 0;
            MinutesPerDay = 60;
        }

        public ServerTime(int Day, int Hour, int MinutesPerDay)
        {
            this.Day = Day;
            this.Hour = Hour % 24; //Validate Hours
            this.MinutesPerDay = MinutesPerDay;
        }

        private float totalHoursPassed = 0;

        public void Update(int realMinutesPased)
        {
            totalHoursPassed += (float)realMinutesPased / MinutesPerDay * 24;

            int daysPased = (int)(totalHoursPassed / 24);
            int hourPased = (int)(totalHoursPassed % 24);

            Day += daysPased;
            Hour += hourPased;

            if (Hour >= 24)
            {
                Day++;
                Hour -= 24;
            }

            totalHoursPassed = totalHoursPassed - daysPased - hourPased;
        }
    }
}
