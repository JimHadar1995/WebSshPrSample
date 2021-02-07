using System;
using System.Linq.Expressions;
using Library.Common.Database.AppSettingsEntity;

namespace Library.Common.Database.Specifications
{
    /// <summary>
    /// 
    /// </summary>
    public class SettingsBaseSpecification : Specification<SettingEntity>
    {
        private readonly string _name;
        public SettingsBaseSpecification(string name)
        {
            _name = name;   
        }

        /// <inheritdoc/>
        public override Expression<Func<SettingEntity, bool>> ToExpression()
            => s => s.Type == _name;
    }
}
