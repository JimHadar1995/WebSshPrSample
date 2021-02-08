using System.ComponentModel.DataAnnotations;

namespace Identity.Application.Dto.Users
{
    /// <summary>
    /// Модель смены пароля для пользователя с указанным идентификатором.
    /// </summary>
    public class ChangePasswordForUserDto
    {
        /// <summary>
        /// Идентификатор пользователя, для которого необходимо сменить пароль
        /// </summary>
        [Required]
        public int UserId { get; set; }

        /// <summary>
        /// Новый пароль.
        /// </summary>
        [Required]
        public string NewPassword { get; set; } = string.Empty;
    }
}
