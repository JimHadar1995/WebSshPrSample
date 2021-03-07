using WebSsh.Enums.Enums;

namespace WebSsh.BlazorClient.Internal.Extensions
{
    public static class StatusExtensions
    {
        public static string ToStringStatus(this UserStatus status)
            => status switch
            {
                UserStatus.Active => "Active",
                UserStatus.Locked => "Locked",
                _ => string.Empty
            };
    }
}
