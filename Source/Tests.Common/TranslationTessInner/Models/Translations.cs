using System.Collections.Generic;

namespace Tests.Common.TranslationTessInner.Models
{
    /// <summary>
    /// Комплект данных для проверки по конкретному файлу констант
    /// </summary>
    public class Translations
    {
        /// <summary>
        /// Имя файла с константами
        /// </summary>
        public string FileName { get; set; } = string.Empty;

        /// <summary>
        /// Значения из констант
        /// </summary>
        public List<string> Values { get; set; } = new List<string>();

        /// <summary>
        /// Значения из файлов-ресурсов
        /// </summary>
        public List<Element> Resources { get; set; } = new List<Element>();
    }
}
