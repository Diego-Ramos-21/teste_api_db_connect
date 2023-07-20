using Serilog;
using Serilog.Core;

namespace Genesis.Integracoes.Integracao_DB.Utils
{
    public class LogHandler
    {
        private static LogHandler instance;
        private static readonly string logFile = "Logs/db.log";
        private Logger FileLog;

        private LogHandler()
        {
            FileLog = new LoggerConfiguration().WriteTo.File(logFile, rollingInterval: RollingInterval.Day).CreateLogger();
        }

        public static Logger GetLogger()
        {
            if (instance == null)
            {
                instance = new LogHandler();
            }
            return instance.FileLog;
        }
    }
}