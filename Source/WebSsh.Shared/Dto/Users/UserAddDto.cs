using System.ComponentModel.DataAnnotations;

namespace WebSsh.Shared.Dto.Users
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    /// <summary>
    /// Модель для создания пользователя
    /// </summary>
    public record UserAddDto
    {
        /// <summary>
        /// Имя пользователя.
        /// </summary>
        [Required]

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
        /// Пароль пользователя. Установка значения возможна только в случае создания пользователя.
        /// </summary>
        public string? Password { get; init; }

        /// <summary>
        /// Идентификаторы ролей пользователя
        /// </summary>
        [Required]
        public int RoleId { get; init; }
    }
}
