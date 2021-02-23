using System;
using System.Collections.Concurrent;
using Renci.SshNet;

namespace WebSsh.Terminal.Models
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public sealed class Session : IDisposable
    {
        /// <summary>
        /// Gets or sets the stored session model.
        /// </summary>
        public ClientConnectionInfo StoredSessionModel { get; set; } = new ClientConnectionInfo();

        /// <summary>
        /// Gets or sets the unique key.
        /// </summary>
        public Guid UniqueKey { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public DateTime StartSessionDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime LastAccessSessionDate { get; set; }

        /// <summary>
        /// Экземпляр клиента
        /// </summary>
        internal SshClient? SshClient { get; set; }

        /// <summary>
        /// Терминал
        /// </summary>
        internal ShellStream? ShellStream { get; set; }

        /// <summary>
        /// Producer-consumer очередь сообщений
        /// </summary>
        public BlockingCollection<string> Lines { get; } = new BlockingCollection<string>();

        /// <summary>
        /// При получении новых данных из терминала
        /// </summary>
        internal Action<OperationResult>? OnLineReceive { get; set; }

        /// <summary>
        /// При ошибке в терминале
        /// </summary>
        internal Action<OperationResult>? OnTerminalError { get; set; }

        /// <inheritdoc />
        public void Dispose()
        {
            try
            {
                ShellStream?.Dispose();
                ShellStream = null;
                SshClient?.Dispose();
                SshClient = null;
                Lines.Dispose();
            }
            catch
            {
                //
            }
        }
    }
}
