using Portfolio.Areas._7DTD.Models;

namespace Portfolio.Areas._7DTD.Data.BloodNightRepository
{
    public interface IBloodNightRepository
    {
        public void InitServerTime(int Day, int Hour, int MinsPerDay);

        public BloodNightView GetView();

    }
}
