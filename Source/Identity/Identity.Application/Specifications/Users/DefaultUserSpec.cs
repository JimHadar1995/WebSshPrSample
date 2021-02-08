using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Identity.Core.Entities;
using Library.Common.Database.Specifications;

namespace Identity.Application.Specifications.Users
{
    /// <summary>
    /// Спецификация определения пользователя по умолчанию
    /// </summary>
    public sealed class DefaultUserSpec : CompositeSpecification<User>
    {
        /// <inheritdoc/>
        public override Expression<Func<User, bool>> ToExpression()
            => _ => _.IsDefaultUser;
    }
}
