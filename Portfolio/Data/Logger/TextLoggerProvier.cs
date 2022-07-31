namespace Portfolio.Data.Logger
{
    public class TextLoggerProvider : ILoggerProvider
    {
        private StreamWriter _writer;

        public TextLoggerProvider(string DirectoryPath = "./Logs/")
        {
            if (Directory.Exists(DirectoryPath) == false) Directory.CreateDirectory(DirectoryPath);

            DirectoryPath += DateTime.Now.ToString("t").Replace('.', '_') + ".txt";
            _writer = new(DirectoryPath);
            _writer.AutoFlush = true;

            _writer.WriteLine($"\n[LoggerPrivider] Initialized: {DateTime.Now.ToString("g")}");
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new TextLogger(_writer);
        }

        public void Dispose()
        {
            _writer.Close();
        }
    }
}
