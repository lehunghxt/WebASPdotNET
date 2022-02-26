namespace Library
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Library.Extensions;

    public class ValueFormatter
    {
        public enum FormattingStyle
        {
            QueryString,
            Content
        };

        public string Format(IDictionary<string, object> keyValues, string separator = ",")
        {
            if (keyValues == null)
            {
                return string.Empty;
            }

            return string.Join(separator, keyValues.Select(x => string.Format("{0}={1}", x.Key, this.FormatContentValue(x.Value))));
        }

        public string Format(IEnumerable<object> keyValues, string separator = ",")
        {
            return string.Join(separator, keyValues.Select(this.FormatContentValue));
        }

        public string FormatContentValue(object value)
        {
            return this.FormatValue(value, FormattingStyle.Content);
        }

        public string FormatQueryStringValue(object value)
        {
            return this.FormatValue(value, FormattingStyle.QueryString);
        }

        /// <summary>
        /// The format value.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="formattingStyle">
        /// The formatting style.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string FormatValue(object value, FormattingStyle formattingStyle)
        {
            return value == null ? "null"
                //: value is string ? string.Format("'{0}'", value)
                : value is DateTime ? ((DateTime)value).ToODataString(formattingStyle)
                : value is DateTimeOffset ? ((DateTimeOffset)value).ToODataString(formattingStyle)
                : value is TimeSpan ? ((TimeSpan)value).ToODataString(formattingStyle)
                : value is Guid ? ((Guid)value).ToODataString(formattingStyle)
                : value is bool ? value.ToString().ToLower()
                : value is long ? ((long)value).ToODataString(formattingStyle)
                : value is ulong ? ((ulong)value).ToODataString(formattingStyle)
                : value is float ? ((float)value).ToString(CultureInfo.InvariantCulture)
                : value is double ? ((double)value).ToString(CultureInfo.InvariantCulture)
                : value is decimal ? ((decimal)value).ToODataString(formattingStyle)
                : value.ToString();
        }
    }
}
