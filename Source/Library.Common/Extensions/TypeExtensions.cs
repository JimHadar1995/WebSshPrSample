using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Library.Common.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Descriptions the specified field information.
        /// </summary>
        /// <param name="fieldInfo">The field information.</param>
        /// <returns></returns>
        public static string Description(this FieldInfo fieldInfo)
        {
            var attrs = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attrs.Any())
            {
                var descrAttr = (DescriptionAttribute)attrs.First();
                return descrAttr.Description;
            }

            return string.Empty;
        }
    }
}
