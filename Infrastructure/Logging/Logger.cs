namespace Infrastructure.Logging
{
    public class Logger
    {
        private readonly string _logFilePath;

        public Logger(string logFilePath)
        {
            _logFilePath = logFilePath;
        }

        public void Log(string message)
        {
            File.AppendAllText(_logFilePath, $"{DateTime.UtcNow}: {message}{Environment.NewLine}");
        }
    }
}
