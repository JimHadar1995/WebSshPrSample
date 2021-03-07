using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace Library.Common.Authentication
{
    /// <summary>
    /// Атрибут с учетом привилегий пользователя
    /// </summary>
    public class JwtBaseAttribute : AuthorizeAttribute
    {        
        public JwtBaseAttribute()
        {
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme;
        }
    }
}
