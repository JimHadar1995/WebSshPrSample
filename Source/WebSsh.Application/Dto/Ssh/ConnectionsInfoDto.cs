#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace WebSsh.Application.Dto.Ssh
{
    /// <summary>
    /// 
    /// </summary>
    public sealed record ConnectionsInfoDto
    {
        /// <summary>
        /// Хост
        /// </summary>

        public string Host { get; init; }
        /// <summary>
        /// Порт
        /// </summary>
        public int Port { get; init; }
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string UserName { get; init; }
        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; init; }
    }
}
