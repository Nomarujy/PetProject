using System.Text.Json;

namespace Portfolio.Utilites.Logger.Json
{
    public class JsonLoggerProvider : ILoggerProvider
    {
        private readonly StreamWriter writer;

        public JsonLoggerProvider(string DirectoryPath = "./Logs/")
        {
            if (Directory.Exists(DirectoryPath) == false) Directory.CreateDirectory(DirectoryPath);

            DirectoryPath += $"{DateTime.UtcNow.ToString("d").Replace('.', '_')}.json";
            writer = new(DirectoryPath, true)
            {
                AutoFlush = true
            };

            var json = JsonSerializer.Serialize(new LogModel("Information", "JsonProvider", null, "Initial json provider"));
            writer.WriteLine(json);
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new JsonLogger<string>(writer, categoryName);
        }

        public void Dispose()
        {
            writer.Close();
            GC.SuppressFinalize(this);
        }
    }
}
