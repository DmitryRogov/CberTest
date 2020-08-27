using System.IO;

namespace CberTest.Core.Models
{
    public class MediaFile
    {
        public string PhotoId { get; }

        /// <summary>
        /// Название
        /// </summary>
        public string FileName { get; }

        /// <summary>
        /// Тип файла
        /// </summary>
        public string FileType { get; }

        /// <summary>
        /// Тип содержимого
        /// </summary>
        public string ContentType { get; }

        /// <summary>
        /// Содержимое файла
        /// </summary>
        public byte[] Content { get; }

        public virtual Photo Photo { get; }

        private MediaFile() { }

        public MediaFile(string fileName, string contentType, byte[] content)
        {
            // TODO: Добавить логику валидации создания объекта
            FileName = fileName;
            ContentType = contentType;
            Content = content;
            FileType = GetExtension(fileName);
        }

        private string GetExtension(string filename)
        {
            //TODO: Зависимость от System.IO
            return Path.GetExtension(filename)?.Replace(".", "").ToLower();
        }
    }
}
