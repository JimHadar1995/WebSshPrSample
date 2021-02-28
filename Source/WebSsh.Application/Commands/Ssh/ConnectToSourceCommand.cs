using MediatR;
using WebSsh.Shared.Dto.Ssh;
using WebSsh.Terminal.Models;

namespace WebSsh.Application.Commands.Ssh
{
    /// <summary>
    /// Команда подключения к устройству по ssh к терминалу
    /// </summary>
    public sealed class ConnectToSourceCommand : IRequest<OperationResult>
    {
        /// <summary>
        /// 
        /// </summary>
        public readonly ConnectionsInfoDto Model;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public ConnectToSourceCommand(ConnectionsInfoDto model)
        {
            Model = model;
        }
    }
}
