namespace Web.Api
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Reflection;
    using System.Web;
    using System.Web.Http;
    using System.Web.Http.Controllers;

    /// <sURMmary>
    /// The odata controller extensions.
    /// </sURMmary>
    public static class ApiControllerExtensions
    {
        #region Parameter
        /// <sURMmary>
        /// Hàm lấy Parameter trả về Object
        /// </sURMmary>
        public static TQueryOption GetParameter<TQueryOption>(this ApiController controller)
        {
            var parameters = GetKeyValues(controller.ControllerContext);
            return CreateQueryParams<TQueryOption>(parameters);
        }

        /// <sURMmary>
        /// Hàm lấy Parameter trả về Dictionary
        /// </sURMmary>
        public static Dictionary<string, string> GetParameter(this ApiController controller)
        {
            var param = new Dictionary<string, string>();
            var parameters = GetKeyValues(controller.ControllerContext);
            foreach (var parameter in parameters)
            {
                if (!parameter.Key.StartsWith("$"))
                    param[parameter.Key] = parameter.Value;
            }

            return param;
        }



        /// <sURMmary>
        /// Hàm lấy Parameter
        /// </sURMmary>
        private static KeyValuePair<string, string>[] GetKeyValues(HttpControllerContext context)
        {
            var queryString = context.Request.RequestUri.Query;
            var parameters = ToKeyValuePairs(HttpUtility.ParseQueryString(queryString)).ToArray();
            return parameters;
        }

        private static TQueryOption CreateQueryParams<TQueryOption>(KeyValuePair<string, string>[] parameters)
        {
            var reportOptionsType = typeof(TQueryOption);
            object options = Activator.CreateInstance(reportOptionsType);
            PropertyInfo[] properties = reportOptionsType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (var parameter in parameters)
            {
                try
                {
                    PropertyInfo propertyInfo =
                        properties.FirstOrDefault(
                            x => string.Equals(x.Name, parameter.Key, StringComparison.CurrentCultureIgnoreCase));
                    if (propertyInfo != null)
                    {
                        propertyInfo.SetValue(
                            options,
                            ChangeObjectType(parameter.Value, propertyInfo.PropertyType),
                            null);
                    }
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException(
                        string.Format(
                            "Cannot set value for parameter Key:{0} Value:{1}",
                            parameter.Key,
                            parameter.Value),
                        ex);
                }
            }

            return (TQueryOption)options;
        }

        private static object ChangeObjectType(string valueString, Type valueType)
        {
            if (string.IsNullOrEmpty(valueString))
            {
                if (valueType.IsValueType)
                {
                    // Create default value for non-nullable type
                    // NOTE: Nullable parameter type call to here is acceptable
                    return Activator.CreateInstance(valueType);
                }

                return null;
            }

            // Get real value type when paramater type is nullable
            if (valueType.IsGenericType && valueType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return Convert.ChangeType(valueString, valueType.GetGenericArguments()[0]);
            }

            return Convert.ChangeType(valueString, valueType);
        }

        private static IEnumerable<KeyValuePair<string, string>> ToKeyValuePairs(NameValueCollection queryString)
        {
            var keyValuePairs = new List<KeyValuePair<string, string>>();
            var keys = queryString.AllKeys;
            foreach (var key in keys)
            {
                var value = queryString[key];
                keyValuePairs.Add(new KeyValuePair<string, string>(key, value));
            }

            return keyValuePairs;
        }
        #endregion
    }
}