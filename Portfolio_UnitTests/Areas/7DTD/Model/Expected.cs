using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio_UnitTests.Areas._7DTD.Model
{
    public class Expected
    {
        public Expected(int day, int hours, int minutes)
        {
            Day = day;
            Hours = hours;
            Minutes = minutes;
        }

        public int Day { get; set; }
        public int Hours { get; set; }
        public int Minutes { get; set; }
    }
}
