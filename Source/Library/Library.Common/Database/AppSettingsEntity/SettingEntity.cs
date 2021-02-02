namespace Library.Common.Database.AppSettingsEntity
{
    /// <summary>
    /// Строка настроек в БД
    /// </summary>
    public class SettingEntity
    {
        /// <summary>
        /// Название настройки.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Название типа из Reflection
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Значение
        /// </summary>
        public string Value { get; set; }
    }
}
