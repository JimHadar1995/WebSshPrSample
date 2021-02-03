using System.ComponentModel.DataAnnotations;

namespace Identity.Application.Dto
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    /// <summary>
    /// 
    /// </summary>
    public record LoginByPassCredentials
    {
        /// <summary>
        /// Имя пользователя.
        /// </summary>
        [Required]
        public string UserName { get; init; }

        /// <summary>
        /// Пароль пользователя.
        /// </summary>
        [Required]
        public string Password { get; init; } 
    }
}
