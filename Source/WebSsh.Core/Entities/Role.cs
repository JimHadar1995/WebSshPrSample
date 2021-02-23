using System.Collections.Generic;
using Library.Common.Database;
using Microsoft.AspNetCore.Identity;

namespace WebSsh.Core.Entities
{
    /// <summary>
    /// Роль пользователя
    /// </summary>
    public class Role : IdentityRole<int>, IAggregateRoot
    {
        /// <summary>
        /// The full administrator role
        /// </summary>
        public const string Administrator = "administrator";

        /// <summary>
        /// The full administrator role
        /// </summary>
        public const string Readonly = "readonly";

        /// <summary>
        /// Описание роли.
        /// </summary>
        public string? Description { get; set; }

        #region [ Navigation ]

        /// <summary>
        /// Пользователи. Многие-ко-многим
        /// </summary>
        public virtual ICollection<UserRole> Users { get; set; }
            = new List<UserRole>();

        #endregion
    }
}
