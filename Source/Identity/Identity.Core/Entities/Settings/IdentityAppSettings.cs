using System;
using System.Threading.Tasks;
using Library.Common.Database;
using Library.Common.Database.AppSettingsEntity;

namespace Identity.Core.Entities.Settings
{
    /// <summary>
    /// Системные настройки
    /// </summary>
    public sealed class IdentityAppSettings : IAppBaseSettingsManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<SettingEntity> _settingsRepo;
        private readonly Lazy<PasswordPolicy> _passwordPolicy;

        /// <summary>
        /// Настройки для парольных политик
        /// </summary>
        public PasswordPolicy PasswordPolicy => _passwordPolicy.Value;

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityAppSettings"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
#pragma warning disable 8618
        public IdentityAppSettings(IUnitOfWork unitOfWork)
#pragma warning restore 8618
        {
            try
            {
                _unitOfWork = unitOfWork;
                _settingsRepo = _unitOfWork.Repository<SettingEntity>();
                _passwordPolicy = new Lazy<PasswordPolicy>(CreateSettings<PasswordPolicy>());
            }
            catch
            {
                //
            }
        }

        /// <inheritdoc />
        public TSetting GetSetting<TSetting>()
            where TSetting : SettingsBase
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public async Task Save()
        {
            if (_passwordPolicy.IsValueCreated)
                await _passwordPolicy.Value.Save(_settingsRepo).ConfigureAwait(false);

            await _unitOfWork.Commit();
        }


        private T CreateSettings<T>() where T : SettingsBase, new()
        {
            var settings = new T();

            if (_unitOfWork.IsTableExists(nameof(settings)))
            {
                settings.Load(_settingsRepo).Wait();
            }

            return settings;
        }
    }
}
