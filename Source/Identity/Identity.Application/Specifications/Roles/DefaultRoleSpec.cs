using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Identity.Core.Entities;
using Library.Common.Database.Specifications;

namespace Identity.Application.Specifications.Roles
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class DefaultRoleSpec : CompositeSpecification<Role>
    {
        /// <inheritdoc/>
        public override Expression<Func<Role, bool>> ToExpression()
            => _ => _.IsDefaultRole;
    }
}
