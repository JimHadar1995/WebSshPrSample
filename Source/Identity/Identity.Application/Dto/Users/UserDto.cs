using System;
using System.Collections.Generic;
using Identity.Application.Dto.Roles;
using Identity.Core.Enums;

namespace Identity.Application.Dto.Users
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    /// <summary>
    /// 
    /// </summary>
    public record UserDto
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public int Id { get; init; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>

        public string UserName { get; init; }

        /// <summary>
        /// Описание пользователя.
        /// </summary>
        public string? Description { get; init; }

        /// <summary>
        /// Электронный адрес пользователя.
        /// </summary>
        public string? Email { get; init; }

        /// <summary>
        /// Дата создания пользователя.
        /// </summary>
        public DateTimeOffset CreatedAt { get; init; }

        /// <summary>
        /// Дата последнего обновления пользователя.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; init; }

        /// <summary>
        /// Время последней авторизации пользователя
        /// </summary>
        public DateTimeOffset? LastLogin { get; init; }

        /// <summary>
        /// Статус пользователя
        /// </summary>
        public UserStatus Status { get; init; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is default user.
        /// </summary>
        public bool IsDefaultUser { get; init; }

        /// <summary>
        /// Язык интерфейса
        /// </summary>
        public Locale UiLocale { get; init; } = Locale.En;

        /// <summary>
        /// Роли пользователя
        /// </summary>
        public List<RoleDto> Roles { get; init; }
    }
}
