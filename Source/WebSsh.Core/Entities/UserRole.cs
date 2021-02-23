using Microsoft.AspNetCore.Identity;

namespace WebSsh.Core.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class UserRole : IdentityUserRole<int>
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual Role Role { get; set; } = null!;

        /// <summary>
        /// 
        /// </summary>
        public virtual User User { get; set; } = null!;
    }
}
