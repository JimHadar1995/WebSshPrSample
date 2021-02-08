using System;
using System.Linq.Expressions;
using Identity.Core.Entities;
using Library.Common.Database.Specifications;

namespace Identity.Application.Specifications.Users
{
    /// <summary>
    /// Спецификация проверки существования пользователя с указнным именем
    /// </summary>
    public sealed class UserWithNameExistsSpec : CompositeSpecification<User>
    {
        private readonly string _name;
        public UserWithNameExistsSpec(string name)
        {
            _name = name.ToLowerInvariant();
        }

        /// <inheritdoc/>
        public override Expression<Func<User, bool>> ToExpression()
            => _ => _.UserName.ToLower() == _name;
    }
}
