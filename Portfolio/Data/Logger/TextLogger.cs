using System.Text;
namespace Portfolio.Data.Logger
{
    public class TextLogger : ILogger, IDisposable
    {
        private readonly StreamWriter _writer;
        private static object _lock = new object();

        public TextLogger(StreamWriter writer)
        {
            _writer = writer;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return this;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            var sb = new StringBuilder();

            sb.Append($"[{DateTime.Now.ToString("g")}]");
            sb.Append(logLevel);
            sb.Append(formatter(state, exception));

            lock (_lock)
            {
                _writer.WriteLine(sb.ToString());
            }
        }
        public void Dispose() { }
    }
}
