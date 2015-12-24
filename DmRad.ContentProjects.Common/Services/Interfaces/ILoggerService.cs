using DmRad.ContentProjects.Common.Enums;

namespace DmRad.ContentProjects.Common.Services.Interfaces
{
    public interface ILoggerService
    {
        /// <summary>
        /// Добавить записи в локальный лог
        /// </summary>
        void AddToLocalLog(LogLevelType logLevelType, string message);
    }
}