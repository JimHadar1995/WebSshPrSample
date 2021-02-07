using System;
using System.Linq.Expressions;
using Identity.Core.Entities;
using Library.Common.Database.Specifications;

namespace Identity.Application.Specifications.Roles
{
    /// <summary>
    /// Спецификация нахождения привилегии по ее имени
    /// </summary>
    public sealed class FindPrivilegeByNameSpec : Specification<Privilege>
    {
        private readonly string _name;
        public FindPrivilegeByNameSpec(string name)
        {
            _name = name.ToLowerInvariant();
        }

        /// <inheritdoc/>
        public override Expression<Func<Privilege, bool>> ToExpression()
            => p => p.Name == _name;
    }
}
