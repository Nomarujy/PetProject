using System.Text.Json;

namespace Portfolio.Utilites.Logger.Json
{
    public class JsonLoggerProvider : ILoggerProvider
    {
        private StreamWriter _writer = null!;
        private int _currentDay = 0;
        private string _root;

        public JsonLoggerProvider(string root = "./Logs")
        {
            _root = root;
            _currentDay = DateTime.UtcNow.Day;

            RefreshWriter(_root);
        }

        public void RefreshWriter(string root)
        {
            _writer?.Close();
            _writer?.Dispose();

            DateTime date = DateTime.UtcNow;

            root += $"/{date.Year}/{date.Month}/";
            if (Directory.Exists(root) == false)
            {
                Directory.CreateDirectory(root);
            }

            root += $"{date.Day}.json";
            _writer = new(root, true)
            {
                AutoFlush = true
            };
        }

        public ILogger CreateLogger(string categoryName)
        {
            if (DateTime.UtcNow.Day != _currentDay)
            {
                RefreshWriter(_root);
            }
            return new JsonLogger<string>(_writer, categoryName);
        }

        public void Dispose()
        {
            _writer.Close();
            GC.SuppressFinalize(this);
        }
    }
}
