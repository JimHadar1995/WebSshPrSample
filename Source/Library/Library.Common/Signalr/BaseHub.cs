using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Library.Common.Signalr
{
    /// <summary>
    /// Базовый хаб.
    /// </summary>
    public abstract class BaseHub : Hub
    {
        /// <inheritdoc />
        public override async Task OnConnectedAsync()
        {
            try
            {
                var userName = Context.User?.Identity?.Name;
                if (!string.IsNullOrWhiteSpace(userName))
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, userName);
                }
            }
            catch
            {
                //
            }

            await base.OnConnectedAsync();
        }

        /// <inheritdoc />
        public override async Task OnDisconnectedAsync(Exception? ex)
        {
            try
            {
                var userName = Context.User?.Identity?.Name;
                if (!string.IsNullOrWhiteSpace(userName))
                {
                    await Groups.RemoveFromGroupAsync(Context.ConnectionId, userName);
                }
            }
            catch
            {
                //
            }

            await base.OnDisconnectedAsync(ex);
        }
    }
}
