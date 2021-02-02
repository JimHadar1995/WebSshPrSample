using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Library.Common.Exceptions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Primitives;

namespace Library.Common.Types.Paging
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ModelBinding.IModelBinder" />
    public class PagingModelBinder : IModelBinder
    {
        private const string RexBrackets = @"\[\d*\]";
        //Define original source data list
        private List<KeyValuePair<string, StringValues>> _kvps = new List<KeyValuePair<string, StringValues>>();

        /// <inheritdoc />
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            //Check and get source data from query string. 
            _kvps = bindingContext.ActionContext.HttpContext.Request.Query.ToList();
            if (!bindingContext.ActionContext.HttpContext.Request.Path.HasValue)
                return Task.CompletedTask;

            var paging = Activator.CreateInstance(bindingContext.ModelType);

            try
            {
                if (paging == null)
                    return Task.CompletedTask;
                //First call for processing primary object
                SetPropertyValues(paging);

                //Assign completed object tree to Model and return it.
                bindingContext.Result = ModelBindingResult.Success(paging);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException is BaseException vEx)
                    bindingContext.ModelState.AddModelError(
                        bindingContext.ModelName, vEx.Message);
                else
                    bindingContext.ModelState.AddModelError(
                        bindingContext.ModelName, ex.Message);
            }

            return Task.CompletedTask;
        }

        private void SetPropertyValues(object obj)
        {
            if (obj == null)
            {
                return;
            }
            //Recursively set PropertyInfo array for object hierarchy
            PropertyInfo[] props = obj.GetType().GetProperties();

            //Set KV Work List for real iteration process so that kvps is not in iteration and
            //its items from kvps can be removed after each iteration

            foreach (var prop in props)
            {
                //Refresh KV Work list from refreshed base kvps list after processing each property
                var kvpsWork = new List<KeyValuePair<string, StringValues>>(_kvps);

                if ((!prop.PropertyType.IsInterface && !prop.PropertyType.IsClass) || prop.PropertyType.FullName == "System.String")
                {
                    //For single or teminal properties.
                    foreach (var item in kvpsWork)
                    {
                        //Ignore any bracket in a name key 
                        var key = item.Key;
                        var keyParts = Regex.Split(key, RexBrackets);
                        if (keyParts.Length > 1) key = keyParts[^1];
                        if (key.ToLowerInvariant() == prop.Name.ToLowerInvariant())
                        {
                            //Populate KeyValueWork and pass it for adding property to object
                            var kvw = new KeyValueWork()
                            {
                                //SW Updates: re-enter prop.Name as Key to map to model CamelCase.  
                                Key = item.Key,
                                //Key = prop.Name,
                                Value = item.Value,
                                SourceKvp = item
                            };
                            AddSingleProperty(obj, prop, kvw);
                            break;
                        }
                    }
                }
                else if (prop.PropertyType.IsClass || prop.PropertyType.IsInterface)
                {
                    if (prop.ToString()!.ToLowerInvariant().Contains("filter"))
                    {
                        var strList = new List<KeyValueWork>();
                        foreach (var item in kvpsWork)
                        {
                            var itemKey = item.Key
                                .Replace("Filter", string.Empty)
                                .Replace("filter", string.Empty)
                                .Replace(".", string.Empty)
                                .Replace(" ", string.Empty);

                            var kvw = new KeyValueWork()
                            {
                                Key = itemKey,
                                Value = item.Value,
                                SourceKvp = item
                            };

                            strList.Add(kvw);
                            if (kvw.SourceKvp.Key.ToLowerInvariant().Contains("filter"))
                            {
                                _kvps.Remove(item);
                            }
                        }

                        var filterProps = prop.PropertyType.GetProperties();
                        foreach (var filterProp in filterProps)
                        {
                            foreach (var kvw in strList)
                            {
                                if (kvw.Key.ToLowerInvariant() == filterProp.Name.ToLowerInvariant())
                                {
                                    AddSingleProperty(prop.GetValue(obj)!, filterProp, kvw);
                                }
                            }
                        }

                    }
                }
            }
        }

        private void AddSingleProperty(object obj, PropertyInfo prop, KeyValueWork item)
        {
            if (prop.PropertyType.IsEnum)
            {
                var enumValues = prop.PropertyType.GetEnumValues();
                object? enumValue = null;
                bool isFound = false;

                //Try to match enum item name first
                for (int i = 0; i < enumValues.Length; i++)
                {
                    if (item.Value.ToLowerInvariant() == enumValues.GetValue(i)?.ToString()!.ToLowerInvariant())
                    {
                        enumValue = enumValues.GetValue(i);
                        isFound = true;
                        break;
                    }
                }
                //Try to match enum default underlying int value if not matched with enum item name
                if (!isFound)
                {
                    foreach (var enumV in enumValues)
                    {
                        if (((int)enumV!).ToString(CultureInfo.CurrentCulture) == item.Value)
                        {
                            enumValue = Convert.ToInt32(item.Value, CultureInfo.CurrentCulture);
                        }
                    }
                }
                prop.SetValue(obj, enumValue, null);
            }
            else
            {
                if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    var genericType = prop.PropertyType.GetGenericArguments()[0];
                    prop.SetValue(obj, Convert.ChangeType(item.Value, genericType, CultureInfo.CurrentCulture), null);
                }
                else
                {
                    if (prop.PropertyType.GetInterface(nameof(IList)) != null)
                    {
                        var genericType = prop.PropertyType.GetGenericArguments()[0];
                        var enumerableType = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(genericType))!;

                        var values = item.Value.Split(",").ToList();
                        values.ForEach(v => enumerableType.Add(Convert.ChangeType(v, genericType, CultureInfo.CurrentCulture)));
                        prop.SetValue(obj, enumerableType);
                    }
                    else
                    {
                        prop.SetValue(obj, Convert.ChangeType(item.Value, prop.PropertyType, CultureInfo.CurrentCulture), null);
                    }
                }
            }
            _kvps.Remove(item.SourceKvp);
        }

        class KeyValueWork
        {
            internal string Key { get; set; } = string.Empty;
            internal string Value { get; set; } = string.Empty;
            internal KeyValuePair<string, StringValues> SourceKvp { get; set; }
        }
    }
}
