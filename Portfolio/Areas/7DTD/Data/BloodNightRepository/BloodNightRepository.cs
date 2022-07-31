using Portfolio.Areas._7DTD.Models;

namespace Portfolio.Areas._7DTD.Data.BloodNightRepository
{
    public class BloodNightRepository : IBloodNightRepository
    {
        private ServerTime _serverTime = null!;
        private Timer _timer = null!;
        private readonly TimeSpan TimerPeriod = new TimeSpan(0, 10, 0);

        public BloodNightRepository() => InitServerTime(0, 0, 60);

        public BloodNightView GetView()
        {
            return new BloodNightView(_serverTime);
        }

        public void InitServerTime(int Day, int Hour, int MinsPerDay)
        {
            _serverTime = new(Day, Hour, MinsPerDay);

            _timer?.Dispose();
            _timer = new Timer(new TimerCallback(Refresh), null, TimerPeriod, TimerPeriod);
        }

        private void Refresh(object? alwaysNull)
        {
            _serverTime.Update(TimerPeriod.Minutes);
        }

    }
}
