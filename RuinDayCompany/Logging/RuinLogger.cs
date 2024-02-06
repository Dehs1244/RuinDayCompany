using RuinDayCompany.Interfaces;
using BepInEx.Logging;

namespace RuinDayCompany.Logging
{
    public class RuinLogger : IRuinLogger
    {
        private readonly ManualLogSource _logger;

        public RuinLogger(ManualLogSource logger)
        {
            _logger = logger;
        }

        public void Log(string message, LogLevel level) =>
            _logger.Log(level, message);

        public void LogError(string message) =>
            Log(message, LogLevel.Error);

        public void LogInfo(string message) =>
            Log(message, LogLevel.Info);

        public void LogMessage(string message) =>
            Log(message, LogLevel.Message);

        public void LogWarn(string message) =>
            Log(message, LogLevel.Warning);
    }
}
