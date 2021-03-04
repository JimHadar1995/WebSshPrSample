using Microsoft.AspNetCore.Components.Routing;

namespace WebSsh.BlazorClient.Internal.Models
{
    /// <summary>
    /// 
    /// </summary>
    public record MenuItem(string Title, string RouterLink, NavLinkMatch NavLinkMatch = NavLinkMatch.All);
}
