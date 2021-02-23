using System;

namespace WebSsh.Terminal.Models
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ClientConnectionInfo
    {
        /// <summary>
        /// Уникальный ключ соединения
        /// </summary>
        public Guid UniqueKey { get; set; } = Guid.NewGuid();
        /// <summary>
        /// Хост
        /// </summary>
        public string Host { get; set; } = string.Empty;
        /// <summary>
        /// Порт
        /// </summary>
        public int Port { get; set; } = 22;
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string UserName { get; set; } = string.Empty;
        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
}
