using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Identity.Core.Entities
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    /// <summary>
    /// Роль пользователя
    /// </summary>
    public class UserRole : IdentityRole<int>
    {
        /// <summary>
        /// The full administrator role
        /// </summary>
        public const string FullAdministratorRole = "GlobalAdministrator";

        /// <summary>
        /// Описание роли.
        /// </summary>
        public string? Description { get; init; }

        /// <summary>
        /// Является ли роль ролью по умолчанию
        /// </summary>
        public bool IsDefaultRole { get; init; }

        #region [ Navigation ]

        /// <summary>
        /// Пользователи. Многие-ко-многим
        /// </summary>
        public ICollection<User> Users { get; init; }
            = new List<User>();

        /// <summary>
        /// Привилегии, доступные для роли. Многие-ко-многим
        /// </summary>
        public ICollection<Privilege> Privileges { get; init; }
            = new List<Privilege>();

        #endregion
    }
}
