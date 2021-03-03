namespace WebSsh.BlazorClient.Internal.Services.Contracts
{
    public interface IMessageService
    {
        /// <summary>
        /// Ошибка
        /// </summary>
        /// <param name="message"></param>
        void Error(string message);

        /// <summary>
        /// Информационное сообщение
        /// </summary>
        /// <param name="message"></param>
        void Info(string message);

        /// <summary>
        /// Предупреждение
        /// </summary>
        /// <param name="message"></param>
        void Warn(string message);
    }
}
