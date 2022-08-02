using System.Text.Json;
namespace Portfolio.Data.Logger
{
    public class JsonLoggerProvider : ILoggerProvider
    {
        private StreamWriter _writer;

        public JsonLoggerProvider(string DirectoryPath = "./Logs/")
        {
            if (Directory.Exists(DirectoryPath) == false) Directory.CreateDirectory(DirectoryPath);

            _writer = new(DirectoryPath, true);
            _writer.AutoFlush = true;

            var json = JsonSerializer.Serialize(new LogModel("Information", "JsonProvider", null, "Initial json provider"));
            _writer.WriteLine(json);
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new JsonLogger<string>(_writer, categoryName);
        }

        public void Dispose()
        {
            _writer.Close();
        }
    }
}
