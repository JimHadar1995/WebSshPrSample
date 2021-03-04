using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using WebSsh.BlazorClient.Internal.Constants;
using WebSsh.BlazorClient.Internal.Models;
using WebSsh.BlazorClient.Internal.Services.Contracts;

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
                new MenuItem("Home", PageUrlConstants.Index)
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
