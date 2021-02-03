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
        public string? Description { get; init; }

        /// <inheritdoc />
        [FullTextSearchProperty]
        public override string UserName { get; set; }

        /// <summary>
        /// Статус пользователя
        /// </summary>
        [Required]
        public UserStatus Status { get; init; }

        /// <summary>
        /// Время создания пользователя.
        /// </summary>
        public DateTimeOffset CreatedAt { get; init; }

        /// <summary>
        /// Время последнего обновления пользователя
        /// </summary>
        public DateTimeOffset UpdatedAt { get; init; }

        /// <summary>
        /// Время последней авторизации пользователя
        /// </summary>
        public DateTimeOffset? LastLogin { get; init; }

        /// <summary>
        /// Время изменения пароля пользователя.
        /// </summary>
        public DateTimeOffset DatePasswordChanged { get; init; }

        /// <summary>
        /// Является ли пользователем по умолчанию, создаваемым системой
        /// </summary>
        public bool IsDefaultUser { get; init; }

        /// <summary>
        /// Флаг, означающий, что пароль был сброшен администратором, вне зависимости от действий пользователя.
        /// </summary>
        public bool PasswordResetedByAdministrator { get; init; }

        /// <summary>
        /// Язык интерфейса
        /// </summary>
        public Locale UiLocale { get; init; }

        #region [ Navigation ]

        /// <summary>
        /// Роли пользователя. Многие-ко-многим
        /// </summary>
        public ICollection<Role> Roles { get; init; }
            = new List<Role>();

        /// <summary>
        /// Gets or sets the hash history.
        /// </summary>
        public ICollection<UserPasswordHashHistory> HashHistory { get; init; }
            = new List<UserPasswordHashHistory>();

        #endregion
    }
}
