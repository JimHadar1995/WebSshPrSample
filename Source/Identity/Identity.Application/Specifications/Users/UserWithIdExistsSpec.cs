using System;
using System.Linq.Expressions;
using Identity.Core.Entities;
using Library.Common.Database.Specifications;

namespace Identity.Application.Specifications.Users
{
    /// <summary>
    /// Спецификация для нахождения пользователя с указанным ID
    /// </summary>
    public sealed class UserWithIdExistsSpec : CompositeSpecification<User>
    {
        private readonly int _id;
        public UserWithIdExistsSpec(int id)
        {
            _id = id;
        }

        /// <inheritdoc/>
        public override Expression<Func<User, bool>> ToExpression()
            => _ => _.Id == _id;
    }
}
