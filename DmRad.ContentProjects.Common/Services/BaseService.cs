using System;
using DmRad.ContentProjects.Common.Enums;
using DmRad.ContentProjects.Common.Tools;
using DmRad.ContentProjects.Database;

namespace DmRad.ContentProjects.Common.Services
{
    public abstract class BaseService
    {
        /// <summary>
        /// Действие для логирования
        /// </summary>
        protected Action<string> LogAction = message =>
        {
            if (Config.DatabaseLogging)
                Endpoint.Instance.LoggerService.AddToLocalLog(LogLevelType.Info, message);
        };

        /// <summary>
        /// Работа с контекстом БД
        /// </summary>
        protected void WithDb(Action<ContentProjectsDb> workingWithDb)
        {
            try
            {
                using (var dbContext = new ContentProjectsDb())
                {
                    dbContext.Database.CommandTimeout = int.MaxValue;

                    if (Config.DatabaseLogging)
                        dbContext.Database.Log = message => Endpoint.Instance.LoggerService.AddToLocalLog(LogLevelType.Info, message);
                    workingWithDb(dbContext);
                }
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                    ex = ex.InnerException;
                Endpoint.Instance.LoggerService.AddToLocalLog(LogLevelType.Error, ex.ToString());

                throw;
            }
        }

        /// <summary>
        /// Работа с контекстом БД с возвращаемым значением
        /// </summary>
        protected TRet WithDb<TRet>(Func<ContentProjectsDb, TRet> workingWithDb)
        {
            TRet ret;

            try
            {
                using (var dbContext = new ContentProjectsDb())
                {
                    ret = workingWithDb(dbContext);
                }
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                    ex = ex.InnerException;
                Endpoint.Instance.LoggerService.AddToLocalLog(LogLevelType.Error, ex.ToString());

                throw;
            }

            return ret;
        }
    }
}
