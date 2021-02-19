using System.Linq;
using System.Threading.Tasks;
using Library.Common.Localization;
using Library.Common.Localization.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

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
