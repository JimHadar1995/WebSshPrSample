using System.ComponentModel.DataAnnotations;

namespace WebSsh.Shared.Dto.Users
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    /// <summary>
    /// 
    /// </summary>
    public record ChangePasswordDto
    {
        /// <summary>
        /// Старый пароль пользователя.
        /// </summary>
        [Required]
        public string OldPassword { get; init; }

        /// <summary>
        /// Новый пароль.
        /// </summary>
        [Required]
        public string NewPassword { get; init; }

        /// <summary>
        /// Повтор нового пароля.
        /// </summary>
        [Required]
        public string ConfirmNewPassword { get; init; }
    }
}
