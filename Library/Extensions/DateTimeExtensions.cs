namespace Library.Extensions
{
    using System;

    /// <summary>
    /// The date time extensions.
    /// </summary>
    internal static class DateTimeExtensions
    {
        /// <summary>
        /// The to iso 8601 string.
        /// </summary>
        /// <param name="dateTime">
        /// The date time.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ToIso8601String(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-ddTHH:mm:ss.fffffffZ");
        }

        /// <summary>
        /// The to iso 8601 string.
        /// </summary>
        /// <param name="dateTimeOffset">
        /// The date time offset.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ToIso8601String(this DateTimeOffset dateTimeOffset)
        {
            return dateTimeOffset.ToString("yyyy-MM-ddTHH:mm:ss.fffffffZ");
        }

        /// <summary>
        /// The to iso 8601 string.
        /// </summary>
        /// <param name="timeSpan">
        /// The time span.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ToIso8601String(this TimeSpan timeSpan)
        {
            return timeSpan.ToString("HH:mm:ss.fffffffZ");
        }

        /// <summary>
        /// The to o data string.
        /// </summary>
        /// <param name="dateTime">
        /// The date time.
        /// </param>
        /// <param name="formattingStyle">
        /// The formatting style.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ToODataString(
            this DateTime dateTime, 
            ValueFormatter.FormattingStyle formattingStyle)
        {
            var value = dateTime.ToString("yyyy-MM-ddTHH:mm:ss.fffffff");
            return string.Format(@"{0}", value);
        }

        /// <summary>
        /// The to o data string.
        /// </summary>
        /// <param name="dateTimeOffset">
        /// The date time offset.
        /// </param>
        /// <param name="formattingStyle">
        /// The formatting style.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ToODataString(
            this DateTimeOffset dateTimeOffset, 
            ValueFormatter.FormattingStyle formattingStyle)
        {
            var value = dateTimeOffset.ToString("yyyy-MM-ddTHH:mm:ss.fffffffZ");
            return string.Format(@"{0}", value);
        }

        /// <summary>
        /// The to o data string.
        /// </summary>
        /// <param name="timeSpan">
        /// The time span.
        /// </param>
        /// <param name="formattingStyle">
        /// The formatting style.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ToODataString(
            this TimeSpan timeSpan, 
            ValueFormatter.FormattingStyle formattingStyle)
        {
            var value = timeSpan.ToString();
            return string.Format(@"{0}", value);
        }
    }
}