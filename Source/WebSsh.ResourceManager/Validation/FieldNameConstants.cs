using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSsh.ResourceManager.Validation
{
    /// <summary>
    /// Константы названий полей для валидаторов.
    /// </summary>
    public sealed class FieldNameConstants
    {
        /// <summary>
        /// Поле "Название"
        /// </summary>
        public const string Name = "Name";

        /// <summary>
        /// Поле "Описание"
        /// </summary>
        public const string Description = nameof(Description);

        /// <summary>
        /// Поле "Пароль пользователя"
        /// </summary>
        public const string UserName = "User name";

        /// <summary>
        /// Поле "Пользователь"
        /// </summary>
        public const string User = "User";

        /// <summary>
        /// Поле "Пароль"
        /// </summary>
        public const string Password = "Password";

        /// <summary>
        /// Поле "Электронная почта"
        /// </summary>
        public const string Email = "Email";

        /// <summary>
        /// Роль
        /// </summary>
        public const string Role = "Role";

        /// <summary>
        /// Новый пароль
        /// </summary>
        public const string NewPassword = "New password";

        /// <summary>
        /// Привилегии
        /// </summary>
        public const string Privileges = "Privileges";

        /// <summary>
        /// Минимальная длина пароля
        /// </summary>
        public const string MinPasswordLength = "Minimum password length";

        /// <summary>
        /// The minimum count chars when editing password
        /// </summary>
        public const string MinCountCharsWhenEditingPassword =
            "Minimum number of characters changed when editing a password";

        /// <summary>
        /// Минимальное время действия пароля
        /// </summary>
        public const string MinPassExpireTime =
            "Minimum Password Expiration Time";

        /// <summary>
        /// Максимальное время действия пароля
        /// </summary>
        public const string MaxPassExpireTime =
            "Maximum Password Expiration Time";

        /// <summary>
        /// Количество хранимых паролей
        /// </summary>
        public const string NumberOfPasswordsStore =
            "Number of passwords stored";

        /// <summary>
        /// Максимальное количество попыток ввода пароля
        /// </summary>
        public const string MaxNumberOfPasswordAttempts =
            "Maximum number of password attempts";

        /// <summary>
        /// Время блокировки пользователя при неверном вводе пароля
        /// </summary>
        public const string UserLockTime =
            "User lock time when password is entered incorrectly";

        /// <summary>
        /// Блокировка пользователя при неактивности,  кол-во дней
        /// </summary>
        public const string BlockUserOnInActiveCountDays =
            "Blocking a user in case of inactivity, number of days";

    }
}
