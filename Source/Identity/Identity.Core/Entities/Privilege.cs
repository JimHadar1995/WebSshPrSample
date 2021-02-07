using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Library.Common.Database;
using Library.Common.Types.Attributes;

namespace Identity.Core.Entities
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    /// <summary>
    /// Привилегия
    /// </summary>
    public class Privilege : IAggregateRoot
    {
        /// <summary>
        /// Идентификатор привилегии
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Название привилегии (на латинице)
        /// </summary>
        [FullTextSearchProperty]
        public string Name { get; set; }

        /// <summary>
        /// Отображаемое название привилегии
        /// </summary>
        [FullTextSearchProperty]
        public string Description { get; set; }

        #region [ Navigation ]

        /// <summary>
        /// Связанные с привилегий роли. Многие-ко-многим
        /// </summary>
        public ICollection<Role> Roles { get; set; }
            = new List<Role>();

        #endregion
    }
}
