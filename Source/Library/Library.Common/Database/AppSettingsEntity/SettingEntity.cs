namespace Library.Common.Database.AppSettingsEntity
{
    /// <summary>
    /// Строка настроек в БД
    /// </summary>
    public record SettingEntity : IAggregateRoot
    {
        /// <summary>
        /// Название настройки.
        /// </summary>

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Name { get; init; }

        /// <summary>
        /// Название типа из Reflection
        /// </summary>
        public string Type { get; init; } 

        /// <summary>
        /// Значение
        /// </summary>
        public string Value { get; init; }
    }
}
