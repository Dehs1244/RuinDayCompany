using BepInEx.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuinDayCompany.Interfaces
{
    public interface IRuinLogger
    {
        void Log(string message, LogLevel level);
        void LogMessage(string message);
        void LogWarn(string message);
        void LogError(string message);
        void LogInfo(string message);
    }
}
