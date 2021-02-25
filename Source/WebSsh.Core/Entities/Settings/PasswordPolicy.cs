using System;
using Library.Common.Database.AppSettingsEntity;
using Microsoft.AspNetCore.Identity;

namespace WebSsh.Core.Entities.Settings
{
    /// <summary>
    /// Настройки парольных политик.
    /// </summary>
    public sealed class PasswordPolicy : SettingsBase
    {
        /// <summary>
        /// Минимальная длина пароля
        /// </summary>
        public int MinimumLength { get; set; } = 10;

        /// <summary>
        /// Максимальная длина пароля.
        /// </summary>
        public int MaximumLength { get; set; } = 50;

        /// <summary>
        /// Цифры обязательны
        /// </summary>
        public bool RequireDigit { get; set; } = true;

        /// <summary>
        /// Нижний регистр обязателен
        /// </summary>
        public bool RequireLowercase { get; set; } = true;

        /// <summary>
        /// Верхний регистр обязателен
        /// </summary>
        public bool RequireUppercase { get; set; } = true;

        /// <summary>
        /// Нечисловые и нецифровые символы обязательны
        /// </summary>
        public bool RequireNonAlphanumeric { get; set; } = false;

        /// <summary>
        /// Максимальное время действия пароля (в днях)
        /// </summary>
        public int RequiredMaxExpireTime { get; set; } = 60;

        /// <summary>
        /// После какого количества неуспешных попыток пользователь блокируется на DefaultLockoutTimeSpan <see cref="DefaultLockoutTimeSpan"/>
        /// минут
        /// </summary>
        public int MaxFailedAccessAttempts { get; set; } = 3;

        /// <summary>
        /// Указывает, на какое количество минут блокируется пользователь в случае неверного ввода пароля MaxFailedAccessAttempts <see cref="MaxFailedAccessAttempts"/> раз 
        /// </summary>
        public int DefaultLockoutTimeSpan { get; set; } = 15;

        /// <summary>
        /// Performs an implicit conversion from <see cref="PasswordPolicy"/> to <see cref="LockoutOptions"/>.
        /// </summary>
        /// <param name="passwordSettings">The password settings.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator LockoutOptions(PasswordPolicy passwordSettings)
            => new LockoutOptions
            {
                AllowedForNewUsers = true,
                DefaultLockoutTimeSpan = TimeSpan.FromMinutes(passwordSettings.DefaultLockoutTimeSpan),
                MaxFailedAccessAttempts = passwordSettings.MaxFailedAccessAttempts,
            };
    }
}
