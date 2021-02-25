using System;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace WebSsh.Application.Dto.Ssh
{
    /// <summary>
    /// 
    /// </summary>
    public sealed record ExecuteCommandModel
    {
        /// <summary>
        /// Уникальный идентификатор сессии
        /// </summary>
        public Guid UniqueSessionId { get; init; }

        /// <summary>
        /// Команда
        /// </summary>
        public string Command { get; init; }
    }
}
