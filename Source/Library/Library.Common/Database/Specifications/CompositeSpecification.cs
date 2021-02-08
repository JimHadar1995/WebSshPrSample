namespace Library.Common.Database.Specifications
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class CompositeSpecification<T> : Specification<T>, ICompositeSpecification<T>
        where T : class, IAggregateRoot
    {
        /// <inheritdoc/>
        public ISpecification<T> And(ISpecification<T> other) => new AndSpecification<T>(this, other);

        /// <inheritdoc/>
        public ISpecification<T> Or(ISpecification<T> other) => new OrSpecification<T>(this, other);

        /// <inheritdoc/>
        public ISpecification<T> Not(ISpecification<T> spec) => new NotSpecification<T>(spec);
    }
}
