using System;
using System.Linq;
using DmRad.ContentProjects.Common.Models;
using DmRad.ContentProjects.Common.Services.Interfaces;
using DmRad.ContentProjects.Common.Tools;
using DmRad.ContentProjects.Database;

namespace DmRad.ContentProjects.Common.Services.Implementation
{
    public class TestDataService : BaseService, ITestDataService
    {
        /// <summary>
        /// Получить список заголовков по номеру страницы
        /// </summary>
        public HeadersModel GetHeadersByPage(int currentPage = 1, Action<string> errorsCallback = null)
        {
            if (currentPage < 1)
            {
                errorsCallback?.Invoke(Consts.ErrorPageMustBeMoreOrEqualsTo1);
                return null;
            }

            var model = new HeadersModel(currentPage);

            try
            {
                WithDb(db =>
                {
                    var testDataCount = db.TEST_DATA_META.FirstOrDefault()?.COUNT_RECORDS ?? 0;
                    model.PageInfo.MaxPage = testDataCount % Consts.CountHeaderRecordsOnPage == 0
                                    ? testDataCount / Consts.CountHeaderRecordsOnPage
                                    : testDataCount / Consts.CountHeaderRecordsOnPage + 1;

                    if (currentPage > model.PageInfo.MaxPage)
                    {
                        errorsCallback?.Invoke(Consts.ErrorPageMoreThanMax);
                        return;
                    }

                    model.Headers.AddRange(db.Database.SqlQuery<HeaderModel>($"EXECUTE {Consts.StoredProcGetRecordsByPage} {((currentPage - 1) * Consts.CountHeaderRecordsOnPage)}, {Consts.CountHeaderRecordsOnPage}"));
                });
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                    ex = ex.InnerException;
                
                errorsCallback?.Invoke(ex.ToString());
            }
            
            return model;
        }

        /// <summary>
        /// Получить запись по идентификатору
        /// </summary>
        public RecordInfoModel GetRecordById(int id, Action<string> errorsCallback)
        {
            var model = new RecordInfoModel();

            try
            {
                WithDb(db =>
                {
                    var record = db.TEST_DATA.FirstOrDefault(td => td.ID == id);
                    if (record != null)
                    {
                        model.Record = new RecordModel
                        {
                            Id = record.ID,
                            Header = record.HEADER,
                            Text = record.TEXT
                        };
                    }

                    //Следующие записи
                    model.NextRecordsIds = db.TEST_DATA.OrderBy(td => td.ID)
                                            .Where(td => td.ID > id)
                                            .Take(Consts.CountRecordsOnContentPage)
                                            .Select(td => td.ID)
                                            .ToList();


                    //Случайные записи
                    var rand = new Random(DateTime.Now.Millisecond);
                    var minId = db.TEST_DATA.Min(td => td.ID);
                    var maxId = db.TEST_DATA.Max(td => td.ID);
                    for (var i = 0; i < Consts.CountRecordsOnContentPage; i++)
                    {
                        int randId;
                        do
                        {
                            randId = rand.Next(minId, maxId);
                        } while (randId % 2 == 1 || !db.TEST_DATA.Any(td => td.ID == randId));
                        model.RandomRecordsIds.Add(randId);
                    }
                });
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                    ex = ex.InnerException;

                errorsCallback?.Invoke(ex.ToString());
            }

            return model;
        }

        /// <summary>
        /// Добавить новую запись
        /// </summary>
        public bool AddNewRecord(RecordModel model, Action<string> errorsCallback)
        {
            if (model.Id < 2 || model.Id % 2 == 1)
            {
                errorsCallback?.Invoke(Consts.ErrorIdMustBeEvenAndMoreThanZero);
                return false;
            }

            var res = false;
            try
            {
                WithDb(db =>
                {
                    if (db.TEST_DATA.Any(td => td.ID == model.Id))
                    {
                        errorsCallback?.Invoke(Consts.ErrorRecordIsAlreadyInDbCannotAddRecord);
                        return;
                    }

                    db.TEST_DATA.Add(new TEST_DATA
                    {
                        ID = model.Id,
                        HEADER = model.Header,
                        TEXT = model.Text
                    });
                    db.SaveChanges();
                    res = true;
                });
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                    ex = ex.InnerException;

                errorsCallback?.Invoke(ex.ToString());
            }

            return res;
        }

        /// <summary>
        /// Удалить запись
        /// </summary>
        public bool DeleteRecord(int id, Action<string> errorsCallback)
        {
            var res = false;

            try
            {
                WithDb(db =>
                {
                    var record = db.TEST_DATA.FirstOrDefault(td => td.ID == id);
                    if (record != null)
                    {
                        db.TEST_DATA.Remove(record);
                        db.SaveChanges();

                        res = true;
                    }
                    else
                        errorsCallback?.Invoke(Consts.ErrorNoRecordInDbWithSuchId);
                });
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                    ex = ex.InnerException;

                errorsCallback?.Invoke(ex.ToString());
            }

            return res;
        }
    }
}
