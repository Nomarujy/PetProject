namespace Portfolio.Utilites.Logger
{
    public class LogModel
    {
        public string Time { get; set; }
        public string LogLevel { get; set; }
        public string? Category { get; set; }
        public string? EventId { get; set; }
        public string Message { get; set; }

        public LogModel(string LogLevel, string? Category, string? EventId, string Message)
        {
            Time = DateTime.UtcNow.ToString("g");
            this.LogLevel = LogLevel;
            this.Category = Category;
            this.EventId = EventId;
            this.Message = Message;
        }
    }
}
