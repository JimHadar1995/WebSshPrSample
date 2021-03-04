using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using WebSsh.BlazorClient.Internal.Constants;
using WebSsh.BlazorClient.Internal.Services.Contracts;

namespace WebSsh.BlazorClient.Internal.CodeComponents
{
    /// <summary>
    /// Компонент, который может иметь параметром список ролей пользователя
    /// </summary>
    [Authorize]
    public abstract class AuthWithRoleSupportComponentBase : ComponentBase
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        [Inject]
        private protected IAuthService _authService { get; init; }
        [Inject]
        private protected NavigationManager _navigationManager { get; init; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        private protected virtual string Roles { get; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            if (!await _authService.IsAuthenticated())
            {
                _navigationManager.NavigateTo(PageUrlConstants.Login);
                return;
            }
            string[] roles = Roles.Split(',') ?? new string[0];
            if (roles.Any() && !await _authService.IsInRoles(roles))
            {
                _navigationManager.NavigateTo(PageUrlConstants.Index);
                return;
            }
            await base.OnInitializedAsync();
        }
    }
}
