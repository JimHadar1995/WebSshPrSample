namespace Library.Common.Localization
{
    /// <summary>
    /// Системный локализатор. Для логирования событий в БД
    /// </summary>
    public interface IOwnSystemLocalizer<T> : IOwnLocalizer<T>
        where T : class
    {
        
    }
}
