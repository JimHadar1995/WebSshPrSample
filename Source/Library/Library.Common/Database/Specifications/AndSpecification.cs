using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Library.Common.Database.Specifications
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AndSpecification<T> : Specification<T>, ICompositeSpecification<T>
        where T : class, IAggregateRoot
    {
        private readonly ISpecification<T> _left;
        private readonly ISpecification<T> _right;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        public AndSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            _right = right;
            _left = left;
        }

        /// <inheritdoc/>
        public override Expression<Func<T, bool>> ToExpression()
        {
            Expression<Func<T, bool>> leftExpression = _left.ToExpression();
            Expression<Func<T, bool>> rightExpression = _right.ToExpression();

            var invokedExpr = Expression.Invoke(rightExpression, leftExpression.Parameters.Cast<Expression>());

            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(leftExpression.Body, invokedExpr), leftExpression.Parameters);
        }
    }
}
