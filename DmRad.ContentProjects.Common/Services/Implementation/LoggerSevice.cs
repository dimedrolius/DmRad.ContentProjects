using DmRad.ContentProjects.Common.Enums;
using DmRad.ContentProjects.Common.Services.Interfaces;
using NLog;

namespace DmRad.ContentProjects.Common.Services.Implementation
{
    public class LoggerService : ILoggerService
    {
        /// <summary>
        /// Добавить записи в локальный лог
        /// </summary>
        public void AddToLocalLog(LogLevelType logLevelType, string message)
        {
            var logger = LogManager.GetCurrentClassLogger();

            switch (logLevelType)
            {
                case LogLevelType.Info:
                    logger.Info(message);
                    break;
                case LogLevelType.Warning:
                    logger.Warn(message);
                    break;
                case LogLevelType.Error:
                    logger.Error(message);
                    break;
            }
        }
    }
}
