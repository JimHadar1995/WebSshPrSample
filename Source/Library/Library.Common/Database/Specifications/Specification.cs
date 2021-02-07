using System;
using System.Linq.Expressions;

namespace Library.Common.Database.Specifications
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Specification<T> : ISpecification<T>
        where T : class, IAggregateRoot
    {
        /// <inheritdoc/>
        public abstract Expression<Func<T, bool>> ToExpression();
    }
}
