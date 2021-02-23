using System;
using System.Collections.Generic;
using FluentValidation;
using Library.Common.Database.AppSettingsEntity;
using Library.Common.Localization;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WebSsh.ResourceManager.Implementations;
using WebSsh.ResourceManager.Validation;
using Xunit;

namespace WebSsh.Tests.Inner
{
    internal static class Helper
    {
        internal static IValidationLocalizer GetValidationLocalizer()
        {
            var options = Options.Create(new LocalizationOptions { ResourcesPath = "" });
            var factory = new ResourceManagerStringLocalizerFactory(options, NullLoggerFactory.Instance);
            var fieldLocalizer = new OwnLocalizer<FieldNameConstants>(factory);
            var localizer = new OwnLocalizer<ValidationConstants>(factory);

            return new ValidationLocalizer(localizer, fieldLocalizer);
        }

        internal static void AssertTrueValidator<T>(this IValidator<T> validator, T instance)
        {
            var validationResult = validator.Validate(instance);
            Assert.True(validationResult.IsValid);
        }

        internal static void AssertFalseValidator<T>(this IValidator<T> validator, T instance)
        {
            var validationResult = validator.Validate(instance);
            Assert.False(validationResult.IsValid);
            Assert.Equal(1, validationResult.Errors.Count);
        }

        internal static void AssertFalseValidatorWithoutCheckCountErrors<T>(this IValidator<T> validator, T instance)
        {
            var validationResult = validator.Validate(instance);
            Assert.False(validationResult.IsValid);
        }

        internal static List<SettingEntity> GetAppSettings<T>(T val)
            where T : SettingsBase
        {
            var type = typeof(T);
            var props = type.GetProperties();

            var result = new List<SettingEntity>();

            foreach (var propertyInfo in props)
            {
                var propertyValue = propertyInfo.GetValue(val, null);
                string? value;
                switch (propertyInfo.PropertyType.Name)
                {
                    case "IEnumerable`1":
                        value = JsonConvert.SerializeObject(propertyValue);
                        break;
                    case "DateTime":
                        value = Convert.ToDateTime(propertyValue).ToString("s");
                        break;
                    default:
                        value = propertyValue?.ToString();
                        break;
                }

                var setting = new SettingEntity { Name = propertyInfo.Name, Value = value ?? string.Empty, Type = type.Name };

                result.Add(setting);
            }

            return result;
        }
    }
}
