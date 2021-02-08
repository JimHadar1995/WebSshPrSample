namespace Identity.ResourceManager
{
    /// <summary>
    /// Для пользователей
    /// </summary>
    public class UsersConstants
    {
        /// <summary>
        /// Сообщение об успешной смене пароля пользователя
        /// </summary>
        public const string ChangePasswordSuccess = "User {0} has successfully changed password";

        /// <summary>
        /// Пароль пользователя {0} сброшен
        /// </summary>
        public const string ResetPasswordSuccess = "Password for user {0} has been reset";

        /// <summary>
        /// Сообщение об ошибке при смене пароля у  пользователя.
        /// - Первый параметр - имя пользователя.
        /// </summary>
        public const string ChangePasswordError = "Failed to change password for user {0}";

        /// <summary>
        /// Сообщение об успешной заблокирования пользователя
        /// </summary>
        public const string LockSuccess = "User {0} successfully blocked";

        /// <summary>
        /// Сообщение об ошибке заблокирования пользователя.
        /// - Первый параметр - имя пользователя.
        /// </summary>
        public const string LockError = "Failed to block user {0}";

        /// <summary>
        /// Сообщение об успешном разблокировании пользователя
        /// </summary>
        public const string UnLockSuccess = "User {0} unlocked successfully";

        /// <summary>
        /// Сообщение об ошибке разблокировании пользователя.
        /// - Первый параметр - имя пользователя.
        /// </summary>
        public const string UnLockError = "Failed to unlock user {0}";

        /// <summary>
        /// The logged success
        /// </summary>
        public const string LoggedSuccess = "User {0} successfully authorized";

        /// <summary>
        /// Сообщение об ошибке создания пользователя.
        /// - Первый параметр - имя пользователя.
        /// </summary>
        public const string UserCreatedError = "Failed to create user {0}";

        /// <summary>
        /// Сообщение об успешном создании пользователя
        /// </summary>
        public const string UserCreatedSuccess = "User {0} has been successfully created";

        /// <summary>
        /// Сообщение об ошибке удаление пользователя.
        /// - Первый параметр - имя пользователя.
        /// - Второй параметр - описание ошибки
        /// </summary>
        public const string UserDeleteError = "Failed to delete user {0}";

        /// <summary>
        /// Сообщение об успешном удалении пользователя
        /// </summary>
        public const string UserDeleteSuccess = "User {0} successfully deleted";

        /// <summary>
        /// Сообщение об ошибке обновлении пользователя.
        /// - Первый параметр - имя пользователя.
        /// - Второй параметр - описание ошибки
        /// </summary>
        public const string UserUpdateError = "Failed to update user";

        /// <summary>
        /// Сообщение об успешном удалении пользователя
        /// </summary>
        public const string UserUpdateSuccess = "User {0} updated successfully";

        /// <summary>
        /// При получении списка пользователей произошла ошибка
        /// </summary>
        public const string GettingTreeUsersError =
            "An error occurred while getting the list of users";

        /// <summary>
        /// При получении настроек парольной политики произошла ошибка
        /// </summary>
        public const string GettingPasswordPolicyError =
            "An error occurred while retrieving the password policy settings";

        /// <summary>
        /// При сохранении настроек парольной политики произошла ошибка
        /// </summary>
        public const string UpdatePasswordPolicyError =
            "Failed to update the password policy settings";
        /// <summary>
        /// При сохранении настроек парольной политики произошла ошибка
        /// </summary>
        public const string UpdatePasswordPolicySuccess =
            "The password policy settings updated successfully";

        /// <summary>
        /// При получении пользователя с ID = {0} произошла ошибка
        /// </summary>
        public const string GettingByIdError =
            "An error occurred while getting user with ID = {0}";

        /// <summary>
        /// При получении настроек пользователя произошла ошибка
        /// </summary>
        public const string GettingUserAppSettingsError = "An error occurred while retrieving user settings";
    }
}
