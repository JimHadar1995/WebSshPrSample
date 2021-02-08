using System;
using System.Linq.Expressions;
using Identity.Core.Entities;
using Library.Common.Database.Specifications;

namespace Identity.Application.Specifications.Roles
{
    /// <summary>
    /// Спецификация опреления сущестования роли с заданным именем
    /// </summary>
    public sealed class RoleWithNameWithNameExistsSpec : Specification<Role>
    {
        private readonly string _roleName;

        /// <inheritdoc/>
        public RoleWithNameWithNameExistsSpec(string roleName)
        {
            _roleName = roleName.ToLowerInvariant();
        }

        /// <inheritdoc/>
        public override Expression<Func<Role, bool>> ToExpression()
            => _ => _.Name.ToLower() == _roleName;
    }
}
