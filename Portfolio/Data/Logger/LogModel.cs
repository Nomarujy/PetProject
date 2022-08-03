namespace Portfolio.Data.Logger
{
    public class LogModel
    {
        public LogModel(string LogLevel, string? Category, string? EventId, string Message)
        {
            Time = DateTime.UtcNow.ToString("g");
            this.LogLevel = LogLevel;
            this.Category = Category;
            this.eventId = eventId;
            this.Message = Message;
        }

        public string Time { get; set; }
        public string LogLevel { get; set; }
        public string? Category { get; set; }
        public string? eventId { get; set; }
        public string Message { get; set; }
    }
}
