using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Portfolio.Models.StartPage
{
    public class MessageModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Subject { get; set; } = null!;
        public string Message { get; set; } = null!;

        [BindNever]
        public DateTime Created { get; set; } = DateTime.UtcNow;

    }
}
