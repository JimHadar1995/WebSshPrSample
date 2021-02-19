using System;
using System.Linq.Expressions;

namespace Library.Common.Database.Specifications
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NotSpecification<T> : Specification<T>, ICompositeSpecification<T>
        where T : class, IAggregateRoot
    {
        private readonly ISpecification<T> _spec;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        public NotSpecification(ISpecification<T> spec)
        {
            _spec = spec;
        }

        /// <inheritdoc/>
        public override Expression<Func<T, bool>> ToExpression()
        {
            var specExpr = _spec.ToExpression();
            var param = specExpr.Parameters;
            var body = Expression.Not(specExpr.Body);
            var expr = Expression.Lambda<Func<T, bool>>(body, param);
            return expr;
        }
    }
}
