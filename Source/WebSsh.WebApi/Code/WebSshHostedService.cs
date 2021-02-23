using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using WebSsh.Terminal.Common;

namespace WebSsh.WebApi.Code
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="IHostedService" />
    public class WebSshHostedService : IHostedService
    {
        private readonly IHostApplicationLifetime _appLifetime;
        private readonly ServerShellPoll _sshPoll;

        private readonly CancellationTokenSource _cts = new CancellationTokenSource();

        /// <summary>
        /// Initializes a new instance of the <see cref="WebSshHostedService"/> class.
        /// </summary>
        public WebSshHostedService(
            IHostApplicationLifetime appLifetime,
            ServerShellPoll sshPoll)
        {
            _appLifetime = appLifetime;
            _sshPoll = sshPoll;
        }

        /// <inheritdoc />
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Voltron server started");

            _appLifetime.ApplicationStarted.Register(OnStarted);
            _appLifetime.ApplicationStopping.Register(OnStopping);
            _appLifetime.ApplicationStopped.Register(OnStopped);

            _sshPoll.Start(_cts.Token);

            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Voltron server stopped");

            _cts.Cancel();


            return Task.CompletedTask;
        }

        private void OnStarted()
        {
        }

        private void OnStopping()
        {
            Console.WriteLine("OnStopping method called.");
        }

        private void OnStopped()
        {
            Console.WriteLine("OnStopped method called.");
            // On Linux if the shutdown is triggered by SIGTERM then that's signaled with the 143 exit code.
            // Suppress that since we shut down gracefully. https://github.com/aspnet/AspNetCore/issues/6526
            Environment.ExitCode = 0;
        }


    }
}
