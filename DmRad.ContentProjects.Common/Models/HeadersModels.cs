using System.Collections.Generic;

namespace DmRad.ContentProjects.Common.Models
{
    /// <summary>
    /// Модель заголовка записи с пагинацией
    /// </summary>
    public class HeadersModel
    {
        public List<HeaderModel> Headers { get; set; }
        public PageModel PageInfo { get; set; }

        public HeadersModel(int currentPage)
        {
            Headers = new List<HeaderModel>();
            PageInfo = new PageModel
            {
                CurrentPage = currentPage
            };
        }
    }

    /// <summary>
    /// Модель заголовка записи
    /// </summary>
    public class HeaderModel
    {
        public int Id { get; set; }
        public string Header { get; set; }
    }
}
