namespace DmRad.ContentProjects.Common.Tools
{
    public class Consts
    {
        #region Configuration file

        public const string DatabaseLogging = "DatabaseLogging";
        public const int CountHeaderRecordsOnPage = 10;
        public const int CountRecordsOnContentPage = 3;

        #endregion

        #region Database

        public const string StoredProcGetRecordsByPage = "dbo.GetRecordsByPage";

        #endregion

        #region Errors

        public const string ErrorPageMustBeMoreOrEqualsTo1 = "Номер страницы должен быть >= 1";
        public const string ErrorPageMoreThanMax = "Запрошенный номер страницы превышает максимальный номер";
        public const string ErrorIdMustBeEvenAndMoreThanZero = "Идентификатор записи должен быть четным и больше 0. Добавить запись невозможно";
        public const string ErrorRecordIsAlreadyInDbCannotAddRecord = "Запись с данным идентификатором уже присутствует в БД. Добавить запись невозможно";
        public const string ErrorNoRecordInDbWithSuchId = "Запись с указанным идентификатором отсутствует в БД";

        #endregion
    }
}
