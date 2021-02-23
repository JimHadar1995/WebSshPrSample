using Library.Common.Database.AppSettingsEntity;
using WebSsh.Core.Entities.Settings;

namespace WebSsh.Core.Services
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
