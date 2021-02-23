using System.ComponentModel.DataAnnotations;

namespace WebSsh.Application.Dto.Users
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    /// <summary>
    /// Модель смены пароля для пользователя с указанным идентификатором.
    /// </summary>
    public record ChangePasswordForUserDto
    {
        /// <summary>
        /// Идентификатор пользователя, для которого необходимо сменить пароль
        /// </summary>
        [Required]
        public int UserId { get; init; }

        /// <summary>
        /// Новый пароль.
        /// </summary>
        [Required]
        public string NewPassword { get; init; }
    }
}
