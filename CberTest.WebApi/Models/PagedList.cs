using System.Collections.Generic;

namespace CberTest.WebApi.Models
{
    /// <summary>
    /// Результат постраничного запроса списка элементов
    /// </summary>
    public class PagedList<T>
    {
        /// <summary>
        /// Элементы списка
        /// </summary>
        public IEnumerable<T> Items { get; set; }

        /// <summary>
        /// Общее количество элементов списка
        /// </summary>
        public int Total { get; set; }
    }
}
