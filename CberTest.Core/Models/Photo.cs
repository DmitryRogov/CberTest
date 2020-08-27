using System;

namespace CberTest.Core.Models
{
    /// <summary>
    /// Фотография медиатеки
    /// </summary>
    public class Photo : BaseEntity<string>
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime Created { get; }

        /// <summary>
        /// Файл
        /// </summary>
        public MediaFile File { get; }

        private Photo() { }

        public Photo(string name, string description, MediaFile file)
        {
            //TODO: Вынести генерацию Id и добавить валидацию
            Id = Guid.NewGuid().ToString();

            //TODO: Возможно лучше использовать нормализованное представление как отдельные поля объекта 
            Name = name.ToLower();
            Description = description.ToLower();
            File = file;
            Created = DateTime.UtcNow;
        }
    }
}
