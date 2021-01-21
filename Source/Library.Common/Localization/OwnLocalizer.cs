using Microsoft.Extensions.Localization;

namespace Library.Common.Localization
{
    /// <inheritdoc cref="IScLocalizer"/>
    public class OwnLocalizer<T> : StringLocalizer<T>,IOwnLocalizer<T>
        where T : class
    {
        /// <inheritdoc />
        public OwnLocalizer(IStringLocalizerFactory factory) : base(factory)
        {
        }
    }
}
