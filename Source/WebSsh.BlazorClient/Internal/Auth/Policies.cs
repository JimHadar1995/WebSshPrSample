using Microsoft.AspNetCore.Authorization;
using WebSsh.BlazorClient.Internal.Constants;

namespace WebSsh.BlazorClient.Internal.Auth
{
    internal static class Policies
    {
        public const string IsAdmin = "Is" + RoleConstants.Administrator;
        public const string IsReadonly = "IsUser" + RoleConstants.Readonly;

        public static AuthorizationPolicy IsAdminPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser()
                                                   .RequireRole(RoleConstants.Administrator)
                                                   .Build();
        }

        public static AuthorizationPolicy IsReadonlyPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser()
                                                   .RequireRole(RoleConstants.Administrator)
                                                   .Build();
        }
    }
}
