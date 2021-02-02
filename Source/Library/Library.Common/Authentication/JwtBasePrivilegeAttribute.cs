using System.Linq;
using System.Threading.Tasks;
using Library.Common.Localization;
using Library.Common.Localization.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Common.Authentication
{
    /// <summary>
    /// Атрибут с учетом привилегий пользователя
    /// </summary>
    public class JwtBasePrivilegeAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        protected readonly string[] Privileges;

        /// <summary>
        /// Initializes a new instance of the <see cref="JwtPrivilegeAttribute"/> class.
        /// </summary>
        /// <param name="privileges">The privileges.</param>
        public JwtBasePrivilegeAttribute(params string[] privileges)
        {
          //  AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme;
            Privileges = privileges;
        }

        /// <inheritdoc />
        public virtual Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            try
            {
                var user = context.HttpContext.User;

                if (user.Identity == null || !user.Identity.IsAuthenticated)
                {
                    return Task.CompletedTask;
                }

                var tokenInfo = context.HttpContext.ParseJwtToken();

                if (!Privileges.Any())
                {
                    return Task.CompletedTask;
                }

                if (tokenInfo == null)
                {
                    context.Result = new ContentResult
                    {
                        StatusCode = StatusCodes.Status403Forbidden
                    };
                    return Task.CompletedTask;
                }

                var privs = tokenInfo!.Privileges
                    .ToList();

                if (!Privileges.Any(privs.Contains))
                {
                    context.Result = new ForbidResult();
                    return Task.CompletedTask;
                }

                // необходима смена пароля
                if (tokenInfo.NeedResetPassword)
                {
                    var localizer = context.HttpContext.RequestServices
                        .GetRequiredService<IOwnLocalizer<CommonLocaleConstants>>();
                    context.Result = new ContentResult
                    {
                        StatusCode = StatusCodes.Status205ResetContent,
                        ContentType = "application/json",
                        Content = localizer[CommonLocaleConstants.ToContinueWorkNeedResetPassword]
                    };
                    return Task.CompletedTask;
                }
            }
            catch
            {
                //
            }

            return Task.CompletedTask;
        }
    }
}
