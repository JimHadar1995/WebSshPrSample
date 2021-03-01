using System.Collections.Generic;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace WebSsh.BlazorClient.Internal.Models.Auth
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthSuccessResult
    {
        /// <summary>
        /// Gets or sets the expires.
        /// </summary>
        public long Expires { get; set; }

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>

        public string UserName { get; set; }

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
        public bool NeedResetPassword { get; set; }

        /// <summary>
        /// Token id
        /// </summary>
        public string TokenId { get; set; }

        /// <summary>
        /// Токен доступа к ресурсам
        /// </summary>
        public string AccessToken { get; set; }
    }
}
