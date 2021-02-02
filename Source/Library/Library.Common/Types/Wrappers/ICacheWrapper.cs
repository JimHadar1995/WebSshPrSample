namespace Library.Common.Types.Wrappers
{
    /// <summary>
    /// Враппер для стандартного cache-in-memory
    /// </summary>
    public interface ICacheWrapper
    {
        /// <summary>
        /// Инициализирован ли кэш
        /// </summary>
        bool IsInitialized { get; }

        /// <summary>
        /// Количество закешированных элементов
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Доступ к содержимому кеша по ключу
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        object this[string key] { get; set; }

        /// <summary>
        /// Возвращает элемент кэша по его строковому ключу
        /// </summary>
        /// <param name="key">Строковый ключ объекта, хранящегося в кжше</param>
        /// <returns>Объект, соответствующий ключу</returns>
        object Get(string key);

        /// <summary>
        /// Получение объекта по его ключу.
        /// Объект записывается в переменную value.
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <param name="value">Объект, полученный по ключу</param>
        /// <returns>True, если объект получен успешно. False, если объект не найден в кэше</returns>
        bool TryGetValue(string key, out object value);

        /// <summary>
        /// Получение объекта по его ключу.
        /// Объект записывается в переменную value.
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <param name="value">Объект, полученный по ключу</param>
        /// <returns>True, если объект получен успешно. False, если объект не найден в кэше</returns>
        bool TryGetValue<T>(string key, out T value);

        /// <summary>
        /// Удаление объекта по его ключу
        /// </summary>
        /// <param name="key">Ключ</param>
        void Remove(string key);

        /// <summary>
        /// Запись объекта в кэш
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void Set(string key, object value);

        /// <summary>
        /// Запись объекта в кэш
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void Set<T>(string key, T value);

    }
}
