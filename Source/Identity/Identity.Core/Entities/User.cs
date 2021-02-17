using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Identity.Core.Enums;
using Library.Common.Database;
using Library.Common.Types.Attributes;
using Microsoft.AspNetCore.Identity;

namespace Identity.Core.Entities
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    /// <summary>
    /// Пользователь
    /// </summary>
    [Table("users")]
    public class User : IdentityUser<int>, IAggregateRoot
    {
        /// <summary>
        /// The default admin
        /// </summary>
        public const string DefaultAdmin = "DefaultAdmin";

        /// <summary>
        /// Описание пользователя
        /// </summary>
        [FullTextSearchProperty]
        public string? Description { get; set; }

        /// <inheritdoc />
        [FullTextSearchProperty]
        public override string UserName { get; set; }

        /// <summary>
        /// Статус пользователя
        /// </summary>
        [Required]
        public UserStatus Status { get; set; }

        /// <summary>
        /// Время создания пользователя.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Время последнего обновления пользователя
        /// </summary>
        public DateTimeOffset UpdatedAt { get; set; }

        /// <summary>
        /// Время последней авторизации пользователя
        /// </summary>
        public DateTimeOffset? LastLogin { get; set; }

        /// <summary>
        /// Время изменения пароля пользователя.
        /// </summary>
        public DateTimeOffset DatePasswordChanged { get; set; }

        /// <summary>
        /// Является ли пользователем по умолчанию, создаваемым системой
        /// </summary>
        public bool IsDefaultUser { get; set; }

        /// <summary>
        /// Флаг, означающий, что пароль был сброшен администратором, вне зависимости от действий пользователя.
        /// </summary>
        public bool PasswordResetedByAdministrator { get; set; }

        /// <summary>
        /// Язык интерфейса
        /// </summary>
        public Locale UiLocale { get; set; }

        #region [ Navigation ]

        /// <summary>
        /// Роли пользователя. Многие-ко-многим
        /// </summary>
        public virtual ICollection<Role> Roles { get; set; }
            = new List<Role>();

        /// <summary>
        /// Gets or sets the hash history.
        /// </summary>
        public virtual ICollection<UserPasswordHashHistory> HashHistory { get; set; }
            = new List<UserPasswordHashHistory>();

        #endregion
    }
}
