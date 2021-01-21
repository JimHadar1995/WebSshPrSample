using System;

namespace Library.Common.Types.Attributes
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
    public sealed class FullTextSearchPropertyAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FullTextSearchPropertyAttribute"/> class.
        /// </summary>
        /// <param name="depth">The depth.</param>
        public FullTextSearchPropertyAttribute(int depth)
        {
            Depth = depth;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FullTextSearchPropertyAttribute"/> class.
        /// </summary>
        public FullTextSearchPropertyAttribute()
        {
            Depth = 1;
        }
        /// <summary>
        /// Gets the depth.
        /// </summary>
        public int Depth { get; }
    }
}
