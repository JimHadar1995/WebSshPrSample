using System;

namespace WebSsh.BlazorClient.Internal.Models.Ssh
{
    /// <summary>
    /// 
    /// </summary>
    public class ActiveSession
    {
        /// <summary>
        /// Идентификатор сессии
        /// </summary>
        public Guid SessionsId { get; init; }

        /// <summary>
        /// Статус сессии
        /// </summary>
        public bool Status { get; set; }
    }
}
