using MediatR;
using WebSsh.Shared.Dto.Ssh;
using WebSsh.Terminal.Models;

namespace WebSsh.Application.Commands.Ssh
{
    /// <summary>
    /// Команда выполнения команды на устройстве
    /// </summary>
    public sealed class ExecuteShellCommand : IRequest<OperationResult>
    {

        /// <summary>
        /// 
        /// </summary>
        public readonly ExecuteCommandModel Model;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExecuteShellCommand"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public ExecuteShellCommand(ExecuteCommandModel model)
        {
            Model = model;
        }
    }
}
