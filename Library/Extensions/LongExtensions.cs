namespace Library.Extensions
{
    using System.Globalization;

    /// <summary>
    /// The long extensions.
    /// </summary>
    internal static class LongExtensions
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
        public static string ToODataString(this long number, ValueFormatter.FormattingStyle formattingStyle)
        {
            var value = number.ToString(CultureInfo.InvariantCulture);
            return string.Format(@"{0}L", value);
        }
    }
}