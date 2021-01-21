using System.Threading.Tasks;

namespace Library.Common.Database.AppSettingsEntity
{
    /// <summary>
    /// Интерфейс для управления настройками приложения
    /// </summary>
    public interface IAppBaseSettingsManager
    {
        /// <summary>
        /// Получение настройки.
        /// </summary>
        /// <typeparam name="TSetting">The type of the setting.</typeparam>
        /// <returns></returns>
        TSetting GetSetting<TSetting>()
            where TSetting : SettingsBase;

        /// <summary>
        /// Сохранить все настройки
        /// </summary>
        Task Save();
    }
}
