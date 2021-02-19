using System.Collections.Generic;

#nullable disable warnings
namespace Library.Common.Authentication.Models
{
    public class JsonWebTokenPayload
    {
        /// <summary>
        /// Gets or sets the expires.
        /// </summary>
        public long Expires { get; init; }

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public int UserId { get; init; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        public string UserName { get; init; } 

        /// <summary>
        /// Идентификатор роли
        /// </summary>
        public string[] Roles { get; set; } = { };

        /// <summary>
        /// Claims
        /// </summary>
        public Dictionary<string, string> Claims { get; set; } = new();

        /// <summary>
        /// Необходим ли сброс пароля.
        /// </summary>
        public bool NeedResetPassword { get; init; }

        /// <summary>
        /// Token id
        /// </summary>
        public string TokenId { get; init; }
    }
}
