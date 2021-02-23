using System;
using System.Linq.Expressions;
using Library.Common.Database.Specifications;
using WebSsh.Core.Entities;

namespace WebSsh.Application.Specifications.Users
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
