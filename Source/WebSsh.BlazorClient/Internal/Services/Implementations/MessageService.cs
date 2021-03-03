using WebSsh.BlazorClient.Internal.Services.Contracts;

namespace WebSsh.BlazorClient.Internal.Services.Implementations
{
    public class MessageService : IMessageService
    {
        private readonly AntDesign.MessageService _antMessageService;
        public MessageService(AntDesign.MessageService antMessageService)
        {
            _antMessageService = antMessageService;
        }


        /// <inheritdoc/>
        public void Error(string message)
        {
            _antMessageService.Error(message);
        }

        /// <inheritdoc/>
        public void Info(string message)
        {
            _antMessageService.Info(message);
        }

        /// <inheritdoc/>
        public void Warn(string message)
        {
            _antMessageService.Warn(message);
        }
    }
}
