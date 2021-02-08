using System;
using System.Linq.Expressions;
using Identity.Core.Entities;
using Library.Common.Database.Specifications;

namespace Identity.Application.Specifications.Logs
{
    /// <summary>
    /// Спецификация для фильтрации логов на основе уровня логов
    /// </summary>
    public sealed class LogsInfoFilterSpec : Specification<LogEntity>
    {
        private readonly string _level;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="level"></param>
        public LogsInfoFilterSpec(string level)
        {
            _level = level;
        }

        /// <inheritdoc/>
        public override Expression<Func<LogEntity, bool>> ToExpression()
            => _ => _.Level == _level;
    }
}
