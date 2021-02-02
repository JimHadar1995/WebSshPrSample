using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Library.Common.Exceptions;

namespace Library.Common.Types.Paging
{
    /// <inheritdoc />
    public abstract class DefaultFilter<T> : IFilter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultFilter{T}"/> class.
        /// </summary>
        public DefaultFilter()
        {

        }
        /// <inheritdoc />
        public SortOrderBy OrderBy { get; set; } = SortOrderBy.ASC;

        private string _sortField = "Id";

        /// <summary>
        /// The allowed default sortable fields
        /// </summary>
        protected static readonly HashSet<string> AllowedDefaultSortableFields =
            typeof(T).GetProperties().Select(_ => _.Name.ToLower(CultureInfo.CurrentCulture)).ToHashSet();

        /// <summary>
        /// Массив полей, по которым разрешена сортировка
        /// </summary>
        protected virtual HashSet<string> AllowedSortingFields => AllowedDefaultSortableFields;

        /// <inheritdoc />
        public string SortField
        {
            get => _sortField;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    var fields = AllowedSortingFields;
                    string lowerValue = value.ToLower(CultureInfo.CurrentCulture);
                    _sortField = lowerValue;
                }
                else
                {
                    _sortField = "id";
                }
            }
        }

        /// <inheritdoc />
        public string Search { get; set; } = "";

        /// <summary>
        /// Throws if invalid sort field.
        /// </summary>
        public void ThrowIfInvalidSortField()
        {
            if (!AllowedSortingFields.Contains(SortField))
            {
                throw new BaseException("The field sort undefined");
            }
        }
    }
}
