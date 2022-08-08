using Utf8Json;

namespace Portfolio.Utilites.Logger.Json
{
    public class JsonLogger<T> : ILogger<T>, IDisposable
    {
        private readonly StreamWriter writer;
        private readonly T category;

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
                Message: formatter(state, exception).ToString());

            string json = JsonSerializer.ToJsonString(model);

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
