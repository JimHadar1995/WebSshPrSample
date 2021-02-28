using System;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace WebSsh.Shared.Dto.Ssh
{
    /// <summary>
    /// 
    /// </summary>
    public sealed record UserSessionDto
    {
        /// <summary>
        /// Уникальный идентификатор сессии
        /// </summary>
        public Guid UniqueKey { get; init; }

        /// <summary>
        /// Информация о соединении
        /// </summary>
        public ConnectionsInfoDto ConnectionInfo { get; init; }

        /// <summary>
        /// Дата старта сессии
        /// </summary>
        public DateTime StartSessionDate { get; init; }

        /// <summary>
        /// время последнего выполнения команд.
        /// </summary>
        public DateTime LastAccessSessionDate { get; init; }
    }
}
