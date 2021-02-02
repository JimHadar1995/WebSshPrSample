using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Identity.Core.Entities
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    /// <summary>
    /// Куакуыр ещлут утешен
    /// </summary>
    public class RefreshToken
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [Key]
        public int Id { get; init; }

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public int UserId { get; init; }

        /// <summary>
        /// Сам refresh token
        /// </summary>
        public string Token { get; init; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTimeOffset CreatedAt { get; init; }

        /// <summary>
        /// Время, когда протухнет refresh token
        /// </summary>
        public DateTimeOffset ExpiredAt { get; init; }

        /// <summary>
        /// 
        /// </summary>
        protected RefreshToken()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="passwordHasher"></param>
        public RefreshToken(User user, IPasswordHasher<User> passwordHasher, DateTimeOffset expiredAt)
        {
            UserId = user.Id;
            User = user;
            CreatedAt = DateTime.UtcNow;
            ExpiredAt = expiredAt;
            Token = CreateToken(user, passwordHasher);
        }

        private static string CreateToken(User user, IPasswordHasher<User> passwordHasher)
            => passwordHasher.HashPassword(user, Guid.NewGuid().ToString("N"))
                .Replace("=", string.Empty)
                .Replace("+", string.Empty)
                .Replace("/", string.Empty);

        #region [ Navigation ]

        /// <summary>
        /// Пользователь, с которым связан данный refresh token
        /// </summary>
        public User User { get; init; } = null!;

        #endregion

    }
}
