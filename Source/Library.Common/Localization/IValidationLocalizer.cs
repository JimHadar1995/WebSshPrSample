namespace Library.Common.Localization
{
    /// <summary>
    /// Интерфейс для локализации в валидаторах
    /// </summary>
    /// <remarks>
    /// Данный интерфейс должен быть реализован в соответствующем микро-сервисе и привязан к классам, в которых содержатся константы сообщений валидации.
    /// </remarks>
    public interface IValidationLocalizer
    {
        /// <summary>
        /// Возвращает локализованное имя на основе названия поля <paramref name="fieldName"/>
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns></returns>
        string Name(string fieldName);

        /// <summary>
        /// Возвращает локализованное собщение на основе шаблона
        /// </summary>
        /// <param name="messageTemplate">Шаблон сообщения.</param>
        /// <param name="args"></param>
        /// <returns></returns>
        string Message(string messageTemplate, params object[] args);

        /// <summary>
        /// Возвращает локализованное собщение на основе шаблона
        /// </summary>
        /// <param name="messageTemplate">Шаблон сообщения.</param>
        /// <returns></returns>
        string Message(string messageTemplate);
    }
}
