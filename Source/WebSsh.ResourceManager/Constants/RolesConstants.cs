namespace WebSsh.ResourceManager.Constants
{
    /// <summary>
    /// Для ролей
    /// </summary>
    public class RolesConstants
    {
        /// <summary>
        /// Сообщение об ошибке создания роли.
        /// - Первый параметр - название роли.
        /// - Второй параметр - описание ошибки
        /// </summary>
        public const string RoleCreatedError = "Failed to create role {0}";

        /// <summary>
        /// Сообщение об успешном создании роли
        /// </summary>
        public const string RoleCreatedSuccess = "Role {0} created successfully";

        /// <summary>
        /// Не удалось удалить роль {0}
        /// </summary>
        public const string RoleDeleteError = "Failed to delete role {0}";

        /// <summary>
        /// Роль {0} успешно удалена
        /// </summary>
        public const string RoleDeleteSuccess = "Role {0} deleted successfully";

        /// <summary>
        /// Сообщение об ошибке обновлении роли.
        /// - Первый параметр - название роли
        /// </summary>
        public const string RoleUpdateError = "Failed to update role {0}";

        /// <summary>
        /// Сообщение об успешном удалении роли
        /// </summary>
        public const string RoleUpdateSuccess = "Role {0} updated successfully";

        /// <summary>
        /// При получении списка привилегий произошла ошибка
        /// </summary>
        public const string GettingPrivilegesError =
            "An error occurred while getting the list of privileges";

        /// <summary>
        /// При получении списка ролей произошла ошибка
        /// </summary>
        public const string GettingRolesError =
            "An error occurred while getting the list of roles";

        /// <summary>
        /// При получении роли с ID = {0} произошла ошибка
        /// </summary>
        public const string GettingRoleByIdError =
            "An error occurred while getting role with ID = {0}";
    }
}
