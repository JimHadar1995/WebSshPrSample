using System.Collections.Generic;
using System.Threading.Tasks;
using WebSsh.BlazorClient.Internal.Constants;
using WebSsh.BlazorClient.Internal.Models;
using WebSsh.BlazorClient.Internal.Services.Contracts;
using Microsoft.AspNetCore.Components.Routing;

namespace WebSsh.BlazorClient.Internal.Services.Implementations
{
    /// <inheritdoc/>
    public class MenuService : IMenuService
    {
        private readonly IAuthService _authService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="authService"></param>
        public MenuService(
            IAuthService authService)
        {
            _authService = authService;
        }

        /// <inheritdoc/>
        public async Task<IReadOnlyList<MenuItem>> GetItems()
        {
            if (await _authService.IsInRoles(new string[] { RoleConstants.Administrator }))
            {
                return GetAdminMenuItems();
            }
            if(await _authService.IsInRoles(new string[] { RoleConstants.Readonly}))
            {
                return GetReadonlyMenuItems();
            }

            return new List<MenuItem>();
        }

        #region [ Help methods ]

        private IReadOnlyList<MenuItem> GetAdminMenuItems()
        {
            return new List<MenuItem>
            {
                new MenuItem("Home", PageUrlConstants.Index),
                new MenuItem("Users", PageUrlConstants.Users, NavLinkMatch.Prefix)
            };
        }

        private IReadOnlyList<MenuItem> GetReadonlyMenuItems()
        {
            return new List<MenuItem>
            {
                new MenuItem("Home", PageUrlConstants.Index)
            };
        }

        #endregion
    }
}
