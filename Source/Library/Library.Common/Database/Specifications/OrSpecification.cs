using System;
using System.Linq;
using System.Linq.Expressions;

namespace Library.Common.Database.Specifications
{
    //https://assets.ctfassets.net/9n3x4rtjlya6/4icgRTX3Mcqs8SkkWm0SKe/6c88c397a918811a225690865d3e861f/Max_Arshinov._Expression_Trees.pdf
    public class OrSpecification<T> : Specification<T>, ICompositeSpecification<T>
        where T : class, IAggregateRoot
    {
        private readonly ISpecification<T> _left;
        private readonly ISpecification<T> _right;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        public OrSpecification(ISpecification<T> left, ISpecification<T> right)
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
            return Expression.Lambda<Func<T, bool>>(Expression.OrElse(leftExpression.Body, invokedExpr), leftExpression.Parameters);
        }
    }
}
