using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Library.Common.Database.Specifications;
using Newtonsoft.Json;

namespace Library.Common.Database.AppSettingsEntity
{
    /// <summary>
    /// Базовый класс настроек
    /// <remarks>
    /// https://developingsoftware.com/how-to-store-application-settings-in-aspnet-mvc-using-entity-framework/
    /// </remarks>
    /// </summary>
    public abstract class SettingsBase
    {
        // 1 name and properties cached in readonly fields
        private readonly string _name;
        private readonly PropertyInfo[] _properties;
        /// <summary>
        /// 
        /// </summary>
        public SettingsBase()
        {
            var type = this.GetType();
            _name = type.Name;
            // 2
            _properties = type.GetProperties();
        }

        /// <summary>
        /// Загрузить настройки.
        /// </summary>
        /// <param name="repository"></param>
        public virtual async Task Load(IRepository<SettingEntity> repository)
        {
            var spec = new SettingsBaseSpecification(_name);
            // ARGUMENT CHECKING SKIPPED FOR BREVITY
            // 3 get settings for this type name
            var settings = await repository.GetAsync(spec);

            foreach (var propertyInfo in _properties)
            {
                // get the setting from the settings list
                var setting = settings.SingleOrDefault(s => s.Name == propertyInfo.Name);
                if (setting != null)
                {
                    if (!propertyInfo.CanWrite)
                    {
                        continue;
                    }
                    switch (propertyInfo.PropertyType.Name)
                    {
                        case "IEnumerable`1":
                            var type = typeof(List<>).MakeGenericType(propertyInfo.PropertyType.GenericTypeArguments[0]);
                            propertyInfo.SetValue(this, string.IsNullOrWhiteSpace(setting.Value) ? null : JsonConvert.DeserializeObject(setting.Value, type));
                            break;
                        case "Nullable`1":
                            propertyInfo.SetValue(this,
                                string.IsNullOrWhiteSpace(setting.Value)
                                    ? null
                                    : Convert.ChangeType(setting.Value,
                                        propertyInfo.PropertyType.GenericTypeArguments[0]));
                            break;
                        default:
                            // 4 assign the setting values to the properties in the type inheriting this class
                            propertyInfo.SetValue(this, Convert.ChangeType(setting.Value, propertyInfo.PropertyType));
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Сохранить все настройки
        /// </summary>
        /// <param name="repository"></param>
        public virtual async Task Save(IRepository<SettingEntity> repository)
        {
            var spec = new SettingsBaseSpecification(_name);
            // 5 load existing settings for this type
            var settings = await repository.GetAsync(spec);

            foreach (var propertyInfo in _properties)
            {
                var propertyValue = propertyInfo.GetValue(this, null);
                var value = propertyInfo.PropertyType.Name switch
                {
                    "IEnumerable`1" => JsonConvert.SerializeObject(propertyValue),
                    "DateTime" => Convert.ToDateTime(propertyValue).ToString("s"),
                    _ => propertyValue?.ToString() ?? string.Empty,
                };
                var setting = settings.SingleOrDefault(s => s.Name == propertyInfo.Name);
                if (setting != null)
                {
                    // 6 update existing value
                    setting.Value = value;
                    await repository.UpdateAsync(setting);
                }
                else
                {
                    // 7 create new setting
                    var newSetting = new SettingEntity
                    {
                        Name = propertyInfo.Name,
                        Type = _name,
                        Value = value,
                    };
                    await repository.CreateAsync(newSetting);
                }
            }
        }
    }
}
