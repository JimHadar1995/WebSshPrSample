using System;
using System.Linq.Expressions;
using Identity.Core.Entities;
using Library.Common.Database.Specifications;

namespace Identity.Application.Specifications.Roles
{
    /// <summary>
    /// Спецификация нахождения по id
    /// </summary>
    public sealed class RoleWithNameWithIdExistsSpec : Specification<Role>, ICompositeSpecification<Role>
    {
        private readonly int _id;
        public RoleWithNameWithIdExistsSpec(int id)
            => (_id) = (id);

        /// <inheritdoc/>
        public override Expression<Func<Role, bool>> ToExpression()
            => _ => _.Id == _id;
    }
}
