using System;
using System.Linq;
using System.Linq.Expressions;

namespace Library.Common.Database.Specifications
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AndSpecification<T> : Specification<T>
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

            BinaryExpression andExpression = Expression.AndAlso(
                leftExpression.Body, rightExpression.Body);

            return Expression.Lambda<Func<T, bool>>(
                andExpression, leftExpression.Parameters.Single());
        }
    }
}
