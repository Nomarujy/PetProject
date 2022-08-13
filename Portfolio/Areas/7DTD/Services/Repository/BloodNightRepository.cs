using Portfolio.Areas._7DTD.Models;

namespace Portfolio.Areas._7DTD.Services.Repository
{
    public class BloodNightRepository : IBloodNightRepository
    {
        private ServerTime _serverTime = null!;
        private Timer _timer = null!;
        private TimeSpan refreshPeriod;

        public BloodNightRepository() => InitServerTime(0, 0, 60);

        public BloodNightView GetView()
        {
            return new BloodNightView(_serverTime);
        }

        public void InitServerTime(int Day, int Hour, int MinsPerDay)
        {
            _serverTime = new(Day, Hour, MinsPerDay);
            refreshPeriod = new TimeSpan(0, (int)_serverTime.MinutesPerHour, 0);

            _timer?.Dispose();
            _timer = new Timer(new TimerCallback(Refresh), null, refreshPeriod, refreshPeriod);
        }

        private void Refresh(object? alwaysNull)
        {
            _serverTime.Update(refreshPeriod.Minutes);
        }

    }
}
