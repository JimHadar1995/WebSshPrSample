using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace Library.Common.Database.AppSettingsEntity
{
    class SettingsBaseSpecification : Specification<SettingEntity>
    {
        public SettingsBaseSpecification(string typeName)
        {
            Query.Where(_ => _.Type == typeName);
        }
    }
}
