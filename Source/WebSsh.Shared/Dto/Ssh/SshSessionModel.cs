using System;
using System.ComponentModel.DataAnnotations;

namespace WebSsh.Shared.Dto.Ssh
{
    /// <summary>
    /// 
    /// </summary>
    public sealed record SshSessionModel
    {
        /// <summary>
        /// Идентификатор сессии
        /// </summary>
        [Required]
        public Guid SessionId { get; init; }
    }
}
