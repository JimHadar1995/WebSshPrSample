using System;
using Library.Common.Database;

namespace Identity.Core.Entities
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    /// <summary>
    /// История изменения пароля пользователя.
    /// </summary>
    public class UserPasswordHashHistory : IAggregateRoot
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Хэш пароля.
        /// </summary>
        public string Hash { get; set; }

        /// <summary>
        /// Дата создания записи.
        /// </summary>
        public DateTimeOffset DateChanged { get; set; }

        #region [ Navigation ]

        /// <summary>
        /// Связанный пользователь.
        /// </summary>
        public virtual User User { get; set; }

        #endregion
    }
}
