using System.ComponentModel.DataAnnotations;

namespace WebSsh.BlazorClient.Internal.Models.ViewModels.Users
{
    /// <summary>
    /// 
    /// </summary>
    public class UserViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        [Required]

        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// Описание пользователя.
        /// </summary>
        public string? Description { get; set; } = string.Empty;

        /// <summary>
        /// Электронный адрес пользователя.
        /// </summary>
        [RegularExpression("^\\S+@\\S+\\.\\S+", ErrorMessage = "Incorrect email specified")]
        public string? Email { get; set; } = string.Empty;

        /// <summary>
        /// Пароль пользователя. Установка значения возможна только в случае создания пользователя.
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Идентификаторы ролей пользователя
        /// </summary>
        [Required]
        public int RoleId { get; set; }
    }
}
