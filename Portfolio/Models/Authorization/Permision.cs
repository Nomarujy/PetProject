using System.Text;

namespace Portfolio.Models.Authorization
{
    public class Permision
    {
        public string Category { get; set; } = null!;

        public bool Create { get; set; } = false;
        public bool Read { get; set; } = false;
        public bool Update { get; set; } = false;
        public bool Delete { get; set; } = false;

        public string GetCRUD()
        {
            var sb = new StringBuilder();
            if (Create) sb.Append('C');
            if (Read) sb.Append('R');
            if (Update) sb.Append('U');
            if (Delete) sb.Append('D');

            return sb.ToString();
        }
    }
}
