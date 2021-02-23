using Library.Common.Signalr;
using Microsoft.AspNetCore.Authorization;

namespace WebSsh.Terminal.Hubs
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Common.Signalr.BaseHub" />
    [Authorize]
    public sealed class SshTerminalHub : BaseHub
    {
        /// <summary>
        /// Название команды для отправки данных на UI
        /// </summary>
        public const string SendTerminalResult = nameof(SendTerminalResult);
    }
}
