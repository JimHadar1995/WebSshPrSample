using System;
using System.Linq.Expressions;

namespace Library.Common.Database.Specifications
{
    /// <summary>
    /// Паттерн спецификация
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISpecification<T>
        where T : class, IAggregateRoot
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="candidate"></param>
        /// <returns></returns>
        bool IsSatisfiedBy(T candidate) => ToExpression().Compile().Invoke(candidate);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Expression<Func<T, bool>> ToExpression();
    }
}
