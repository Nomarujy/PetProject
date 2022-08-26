using System.ComponentModel.DataAnnotations;

namespace Portfolio.Areas._7DTD.Models
{
    public class InputForm
    {
        [Range(1, 7)]
        public int Day { get; set; }
        [Range(0, 23)]
        public int Hour { get; set; }
        [Range(1, 1000)]
        public int MinsPerDay { get; set; }
    }
}
