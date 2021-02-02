namespace Library.Common.Types.Paging
{
    /// <summary>
    /// Общий интерфейс фильтра
    /// </summary>
    public interface IFilter
    {
        /// <summary>
        /// Сортировка по убыванию / возрастанию.
        /// </summary>
        SortOrderBy OrderBy { get; set; }

        /// <summary>
        /// По какому полю сортировать.
        /// </summary>
        string SortField { get; set; }

        /// <summary>
        /// Поле поиска.
        /// </summary>
        string Search { get; set; }
    }
}
