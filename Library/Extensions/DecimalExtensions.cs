namespace Library.Extensions
{
    using System.Globalization;

    /// <summary>
    /// The decimal extensions.
    /// </summary>
    internal static class DecimalExtensions
    {
        /// <summary>
        /// The to o data string.
        /// </summary>
        /// <param name="number">
        /// The number.
        /// </param>
        /// <param name="formattingStyle">
        /// The formatting style.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ToODataString(this decimal number, ValueFormatter.FormattingStyle formattingStyle)
        {
            var value = number.ToString("F", CultureInfo.InvariantCulture);
            return string.Format(@"{0}M", value);
        }
    }
}