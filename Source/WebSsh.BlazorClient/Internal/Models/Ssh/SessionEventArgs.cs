using System;

namespace WebSsh.BlazorClient.Internal.Models.Ssh
{
    /// <summary>
    /// 
    /// </summary>
    public class SessionEventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid SessionKey { get; init; }

        public SessionEventArgs(Guid sessionKey)
        {
            SessionKey = sessionKey;
        }
    }
}
