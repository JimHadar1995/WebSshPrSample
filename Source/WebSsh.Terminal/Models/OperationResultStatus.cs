namespace WebSsh.Terminal.Models
{
    /// <summary>
    /// 
    /// </summary>
    public enum OperationResultStatus
    {
        /// <summary>
        /// Нет ошибок
        /// </summary>
        NoError,
        /// <summary>
        /// Ошибка соединения
        /// </summary>
        ConnectionError,
        /// <summary>
        /// Ошибка получения данных
        /// </summary>
        ReceiveDataError,
        /// <summary>
        /// Сессия закрыта
        /// </summary>
        SessionClosed,
        /// <summary>
        /// Ошибка сессии
        /// </summary>
        SessionError
    }
}
