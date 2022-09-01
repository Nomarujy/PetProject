namespace Portfolio.Areas.HelloKafka.Models
{
    public class UserMessage
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Message { get; set; } = null!;

        public string Html { get; set; } = null!;
    }
}
