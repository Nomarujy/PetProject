using System.Text.Json;

namespace Portfolio.Data.Logger
{
    public class JsonLogger<T> : ILogger<T>, IDisposable
    {
        private readonly StreamWriter writer;
        private T category;

        public JsonLogger(StreamWriter Writer, T Category)
        {
            writer = Writer;
            category = Category;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return this;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }


        private static readonly object _lock = new();

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            LogModel model = new(
                LogLevel: logLevel.ToString(), 
                Category: category?.ToString(),
                EventId: eventId.ToString(), 
                Message: formatter(state,
                exception));

            var json = JsonSerializer.Serialize(model);

            lock (_lock)
            {
                writer.WriteLine(json);
            }
        }

        public void Dispose() 
        {
            GC.SuppressFinalize(this);
        }
    }
}
