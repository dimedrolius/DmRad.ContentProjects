using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DmRad.ContentProjects.Common.Models
{
    /// <summary>
    /// Модель записи с информацией о следующих и случайных записях
    /// </summary>
    public class RecordInfoModel
    {
        public RecordModel Record { get; set; }
        public List<int> NextRecordsIds { get; set; }
        public List<int> RandomRecordsIds { get; set; }

        public RecordInfoModel()
        {
            NextRecordsIds = new List<int>();
            RandomRecordsIds = new List<int>();
        }
    }

    /// <summary>
    /// Модель записи
    /// </summary>
    public class RecordModel
    {
        [Display(Name = "Номер:")]
        [Required(ErrorMessage = "Введите номер")]
        public int Id { get; set; }
        [Display(Name = "Заголовок:")]
        [Required(ErrorMessage = "Введите заголовок")]
        public string Header { get; set; }
        [Display(Name = "Текст:")]
        [Required(ErrorMessage = "Введите текст")]
        public string Text { get; set; }

        public bool AddedSuccess { get; set; }
    }
}
