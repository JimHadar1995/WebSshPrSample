using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Identity.Core.Entities
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
