using System;
using System.ComponentModel.DataAnnotations;

namespace Identity.Core.Entities
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    /// <summary>
    /// История изменения пароля пользователя.
    /// </summary>
    public class UserPasswordHashHistory
    {
        /// <summary>
        /// Идентификатор записи.
        /// </summary>
        [Key]
        public long Id { get; init; }

        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public int UserId { get; init; }

        /// <summary>
        /// Хэш пароля.
        /// </summary>
        public string Hash { get; init; }

        /// <summary>
        /// Дата создания записи.
        /// </summary>
        public DateTimeOffset DateChanged { get; init; }

        #region [ Navigation ]

        /// <summary>
        /// Связанный пользователь.
        /// </summary>
        public User User { get; init; }

        #endregion
    }
}
