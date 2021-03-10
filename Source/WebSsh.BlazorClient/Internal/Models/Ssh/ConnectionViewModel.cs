using System.ComponentModel.DataAnnotations;

namespace WebSsh.BlazorClient.Internal.Models.Ssh
{
    /// <summary>
    /// 
    /// </summary>
    public class ConnectionViewModel
    {
        /// <summary>
        /// Хост
        /// </summary>
        [Required]
        public string Host { get; set; } = string.Empty;
        /// <summary>
        /// Порт
        /// </summary>
        [Required]
        public int Port { get; set; } = 22;
        /// <summary>
        /// Имя пользователя
        /// </summary>
        [Required]
        public string UserName { get; set; } = string.Empty;
        /// <summary>
        /// Пароль
        /// </summary>
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
