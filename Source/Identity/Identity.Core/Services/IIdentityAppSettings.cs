using Identity.Core.Entities.Settings;
using Library.Common.Database.AppSettingsEntity;

namespace Identity.Core.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IIdentityAppSettings : IAppBaseSettingsManager
    {
        /// <summary>
        /// Парольные политики
        /// </summary>
        public PasswordPolicy PasswordPolicy { get; }
    }
}
