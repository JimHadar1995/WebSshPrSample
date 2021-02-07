namespace Library.Common.Database.Specifications
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICompositeSpecification<T> : ISpecification<T>
        where T : class, IAggregateRoot
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        ISpecification<T> And(ISpecification<T> other) => new AndSpecification<T>(this, other);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        ISpecification<T> Or(ISpecification<T> other) => new OrSpecification<T>(this, other);
    }
}
