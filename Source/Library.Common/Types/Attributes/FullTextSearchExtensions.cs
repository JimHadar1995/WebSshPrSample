using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text.RegularExpressions;
using Library.Common.Types.Wrappers;

namespace Library.Common.Types.Attributes
{
    /// <summary>
    /// 
    /// </summary>
    public static class FullTextSearchExtensions
    {
        private const string KEY_FULL_TEXT_SEARCH = "{932DB32F-30C2-425E-B0C6-13697FE3C1E9}";

        private static readonly object _CacheLock = new object();

        /// <summary>
        /// Removes the special characters.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static string RemoveSpecialCharacters(this string str)
        {
            return str.Replace("\\", string.Empty).Replace("'", @"\'").Replace("\"", @"\'").Trim();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="props"></param>
        /// <returns></returns>
        public static Dictionary<string, PropertyInfo> WrapQuery(Dictionary<string, PropertyInfo> props)
        {
            Dictionary<string, PropertyInfo> strings = new Dictionary<string, PropertyInfo>();

            Regex regex = new Regex(Regex.Escape("[]"));

            foreach (KeyValuePair<string, PropertyInfo> prop in props.Where(x => x.Value.PropertyType == typeof(string)))
            {
                string query;

                if (prop.Key.Contains("[]"))
                {
                    string propPath = prop.Key.Replace("[].", "[]");

                    string propName = propPath;

                    string[] arr = propName.Split("[]".ToCharArray());

                    if (arr.Length > 0)
                    {
                        propName = arr[^1];
                    }

                    query = propPath + " != null && " + propName + ".ToLower().Contains(\"{0}\".ToLower())";

                    while (query.Contains("[]"))
                    {
                        query = regex.Replace(query, ".Where(", 1);
                        query += ").Any()";
                    }
                }
                else
                {
                    query = prop.Key + " != null && " + prop.Key + ".ToLower().Contains(\"{0}\".ToLower())";
                }

                strings.Add(query, prop.Value);
            }

            return strings;
        }

        /// <summary>
        /// Gets the attributed properties.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="props">The props.</param>
        /// <param name="currentProp">The current property.</param>
        /// <param name="isCollection">if set to <c>true</c> [is collection].</param>
        /// <param name="depth">The depth.</param>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public static Dictionary<string, PropertyInfo> GetAttributedProperties(Type type, Dictionary<string, PropertyInfo>? props = null, PropertyInfo? currentProp = null, bool isCollection = false, int depth = 1, string path = "")
        {
            if (props == null)
            {
                props = new Dictionary<string, PropertyInfo>();
            }

            if (depth >= 0)
            {
                foreach (var prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    var attr = prop.GetCustomAttribute<FullTextSearchPropertyAttribute>();

                    string newPath = path;

                    if (attr != null)
                    {
                        var collectionType = prop.PropertyType.GetInterface("IEnumerable`1");

                        if (currentProp != null)
                        {
                            if (collectionType != null && prop.PropertyType != typeof(String))
                            {
                                newPath = newPath + "." + prop.Name + "[]";
                            }
                            else
                            {
                                newPath = newPath + "." + prop.Name;
                            }
                        }
                        else
                        {
                            if (collectionType != null && prop.PropertyType != typeof(String))
                            {
                                newPath = prop.Name + "[]";
                            }
                            else
                            {
                                newPath = prop.Name;
                            }
                        }

                        if (!props.ContainsKey(newPath))
                        {
                            props.Add(newPath, prop);

                            if (prop.PropertyType != typeof(String) && collectionType != null)
                            {
                                Type collectionEntryType = collectionType.GetGenericArguments()[0];

                                GetAttributedProperties(collectionEntryType, props, prop, true, depth: attr.Depth - 1, path: newPath);
                            }
                            else
                            {
                                GetAttributedProperties(prop.PropertyType, props, prop, depth: attr.Depth - 1, path: newPath);
                            }
                        }
                    }
                }
            }

            return props;
        }
        /// <summary>
        /// Fulls the text search.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <param name="searchStr">The search string.</param>
        /// <param name="cache">The cache.</param>
        /// <returns></returns>
        public static IQueryable<T> FullTextSearch<T>(this IQueryable<T> query, string searchStr, ICacheWrapper cache)
        {
            return (IQueryable<T>)FullTextSearch((IQueryable)query, searchStr, cache);
        }

        /// <summary>
        /// Поиск по заданным полям, при этом поля должны быть помечены атрибутом
        /// FullTextSearchPropertyAttribute
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="searchStr">The search string.</param>
        /// <param name="cache">The cache.</param>
        /// <param name="depth"></param>
        /// <returns></returns>
        public static IQueryable FullTextSearch(this IQueryable query, string searchStr, ICacheWrapper cache, int depth = 1)
        {
            if (String.IsNullOrEmpty(searchStr) || String.IsNullOrWhiteSpace(searchStr))
            {
                return query;
            }
            var type = query.GetType().GetGenericArguments()[0];

            Dictionary<string, PropertyInfo> props;

            lock (_CacheLock)
            {
                if (!cache.TryGetValue<Dictionary<Type, Dictionary<string, PropertyInfo>>>(KEY_FULL_TEXT_SEARCH,
                    out var dic))
                {
                    dic = new Dictionary<Type, Dictionary<string, PropertyInfo>>();
                    cache[KEY_FULL_TEXT_SEARCH] = dic;
                }

                if (!dic.ContainsKey(type))
                {
                    dic.Add(type, WrapQuery(GetAttributedProperties(type, depth: depth)));
                }

                props = dic[type];
            }

            if (props.Count != 0)
            {
                searchStr = searchStr.RemoveSpecialCharacters();
                string criteria = string.Format(string.Join(" or ", props.Select(_ => _.Key)), searchStr);

                return query.Where(criteria);
            }

            return query;
        }

    }
}
