using System;
using DmRad.ContentProjects.Common.Models;

namespace DmRad.ContentProjects.Common.Services.Interfaces
{
    public interface ITestDataService
    {
        /// <summary>
        /// Получить список заголовков по номеру страницы
        /// </summary>
        HeadersModel GetHeadersByPage(int currentPage, Action<string> errorsCallback);
        /// <summary>
        /// Получить запись по идентификатору
        /// </summary>
        RecordInfoModel GetRecordById(int id, Action<string> errorsCallback);
        /// <summary>
        /// Добавить новую запись
        /// </summary>
        bool AddNewRecord(RecordModel model, Action<string> errorsCallback);
        /// <summary>
        /// Удалить запись
        /// </summary>
        bool DeleteRecord(int id, Action<string> errorsCallback);
    }
}
