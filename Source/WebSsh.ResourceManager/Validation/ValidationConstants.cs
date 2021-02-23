namespace WebSsh.ResourceManager.Validation
{
    /// <summary>
    /// Константы для валидаторов.
    /// </summary>
    public sealed class ValidationConstants
    {
        /// <summary>
        /// Сообщение о максимальной длине поля в 50 символов
        /// </summary>
        public const string Max50LengthMessageTemplate = "Field length '{PropertyName}' cannot greater 50 symbols";

        /// <summary>
        /// Сообщение о максимальной длине поля в 250 символов
        /// </summary>
        public const string Max250LengthMessageTemplate = "Field length '{PropertyName}' cannot greater 250 symbols";

        /// <summary>
        /// Сообщение об обязательности заполнения поля
        /// </summary>
        public const string NotEmptyMessageTemplate = "Field '{PropertyName}' is required";

        /// <summary>
        /// Сообщение о том, что поле может содержать только латинские буквы, цифры и спец символы -, _
        /// </summary>
        public const string OnlyLatinAndSpecSymbolsTemplate =
            "Field '{PropertyName}' can contains only latin alphabets, numbers and spec symbols:  -, _";

        /// <summary>
        /// Сообщение об уникальности поля
        /// </summary>
        public const string EntityMustBeUniqueTemplate = "Field '{PropertyName}' must be unique";

        /// <summary>
        /// Сообщение о том, что роли не существуют
        /// </summary>
        public const string RoleNotFoundTemplate = "Roles not exists";

        /// <summary>
        /// Значения полей "Новый пароль"  и "Подтверждение пароля" должны совпадать
        /// </summary>
        public const string NewPassAndConfirmMustBeEqual =
            "Field 'New password' and field 'Confirm password' must be equals";

        /// <summary>
        /// Текущий пароль введен неверно
        /// </summary>
        public const string CurrentPasswordIncorrectTemplate =
            "Incorrect password entered";

        /// <summary>
        /// Новый пароль и текущий не должны совпадать
        /// </summary>
        public const string OldAndNewPassMatch =
            "Old and new passwords must not match";

        /// <summary>
        /// Неверный логин или пароль
        /// </summary>
        public const string InvalidLoginOrPassword =
            "Invalid login or password";

        /// <summary>
        /// Не найден сервисный пользователь
        /// </summary>
        public const string NotFoundServiceAdministrator =
            "Service user not found";

        /// <summary>
        /// Пользователь заблокирован.
        /// </summary>
        public const string UserLocked =
            "User locked";

        /// <summary>
        /// Для роли не указано ни одной привилегии
        /// </summary>
        public const string NoPrivilegesSpecifiedForRole =
            "No privileges specified for the role";

        /// <summary>
        /// Изменение предустановленной роли запрещено
        /// </summary>
        public const string ChangingDefaultRoleIsProhibited =
            "Changing the preset role is prohibited";

        /// <summary>
        /// Изменение предустановленного пользователя запрещено
        /// </summary>
        public const string ChangingDefaultUserIsProhibited =
            "Changing the preset user is prohibited";

        /// <summary>
        /// Удаление предустановленного пользователя запрещено
        /// </summary>
        public const string RemovingDefaultUserIsProhibited =
            "Removing the preset user is prohibited";

        /// <summary>
        /// Удаление предустановленнй роли запрещено
        /// </summary>
        public const string RemovingDefaultRoleIsProhibited =
            "Removing the preset role is prohibited";

        /// <summary>
        /// Сообщение о том, что значение поля должно быть больше указанного значения
        /// </summary>
        public const string MoreOrEqualTemplate =
            "Field value '{PropertyName}' must be greater or equal";

        /// <summary>
        /// Сообщение валидации о том, что максимальное значение поля не должно превышать констатное количество
        /// </summary>
        public const string MaxOrEqualTemplate =
            "Field '{PropertyName}' value must be less or equal";

        /// <summary>
        /// Значение поля 'Минимальное количество измененных символов при редактировании пароля' не может быть больше или равно значения поля 'Минимальная длина пароля'
        /// </summary>
        public const string MinChangedCharsMustBeGreaterMinPassLength =
            "The value of the field 'Minimum number of changed password characters on edit' cannot be greater than or equal to the value of the field 'Minimum password length'";

        /// <summary>
        /// Значение поля 'Минимальное время действия пароля' не должно превышать значение поля 'Максимальное время действия пароля'
        /// </summary>
        public const string MinPassLengthMustBeLessMaxPassLength =
            "The value of the field 'Minimum password age' shall not exceed the value of the field 'Maximum password age'";

        /// <summary>
        /// Сообщение о том, что значение поля должно быть больше нуля
        /// </summary>
        public const string MoreZeroTemplate =
            "Field value '{PropertyName}' must be greater 0";

        /// <summary>
        /// Сообщение о том, что поле должно содержать цифры
        /// </summary>
        public const string ContainsDigitsTemplate = "Field '{PropertyName}' must be contains digits";

        /// <summary>
        /// Сообщение о том, что поле должно содержать символы нижнего регистра
        /// </summary>
        public const string ContainsLowerCaseTemplate = "Field '{PropertyName}' must be contains symbols in lower case";

        /// <summary>
        /// Сообщение о том, что поле должно содержать символы верхнего регистра
        /// </summary>
        public const string ContainsUpperCaseTemplate = "Field '{PropertyName}' must be contains symbols in upper case";

        /// <summary>
        /// Сообщение о том, что поле должно содержать нецифровые и нечисловые символы
        /// </summary>
        public const string ContainsNonAlphaNumericTemplate =
            "Field '{PropertyName}' must be contains not alfa symbols, not numeric symbols";

        /// <summary>
        /// Сообщение о минимальной и максимальной длине пароля
        /// </summary>
        public const string PasswordRequiredLength =
            "Field 'Password' value length must be contains minimum {0} and maximum {1} symbols";

        /// <summary>
        /// Минимальное количество измененных символов в пароле должно быть {0}
        /// </summary>
        public const string MinChangedCharsError =
            "The minimum number of characters changed in the password must be {0}";

        /// <summary>
        /// Администратор {0} заблокирован из-за превышения количества попыток ввода пароля на {1} мин.
        /// </summary>
        public const string AdminLockedNumberAttempts =
            "Admin {0} was blocked due to exceeding the number of password attempts by {1} min.";

        /// <summary>
        /// Блокировка предустановленного администратора запрещена
        /// </summary>
        public const string DefaultUserLockIsProhibited =
            "Preset administrator lock disabled";

        /// <summary>
        /// Для продолжения работы с системой необходимо сменить пароль
        /// </summary>
        public const string ToContinueWorkNeedResetPassword =
            "To continue working with the system, you must change the password";

        /// <summary>
        /// Новый пароль уже был использован ранее. Пожалуйста введите другой пароль
        /// </summary>
        public const string NewPasswordHasBeenUsedBefore =
            "The new password has already been used before. Please enter a different password";

        /// <summary>
        /// Сообщение о том, что пользователь AD не найден
        /// </summary>
        public const string LdapUserNotFound =
            "LDAP user not found";

        /// <summary>
        /// Сообщение о том, что невозможно привязать администратора к пользователю AD
        /// </summary>
        public const string CannotTieAdminToAdUser =
            "Cannot tie admin to LDAP user";

        /// <summary>
        /// Настройки подключения к домену LDAP заданы некорректно
        /// </summary>
        public const string InCorrectLdapConnectionSettings =
            "LDAP domain connection settings are incorrect";

        /// <summary>
        /// Группа пользователей не найдена
        /// </summary>
        public const string UserGroupNotFound =
            "User group not found";

        /// <summary>
        /// Пользователь с ID = {0} не найден
        /// </summary>
        public const string UserNotFound =
            "User with ID = {0} not found";

        /// <summary>
        /// Пароль содержит один из популярных паролей.
        /// </summary>
        public const string PasswordContainsPopularPassword =
            "The password contains one of the popular passwords.";
    }
}
