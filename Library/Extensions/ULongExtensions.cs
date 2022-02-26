namespace Library.Extensions
{
    using System.Globalization;

    /// <summary>
    /// The u long extensions.
    /// </summary>
    internal static class ULongExtensions
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
        public static string ToODataString(this ulong number, ValueFormatter.FormattingStyle formattingStyle)
        {
            var value = number.ToString(CultureInfo.InvariantCulture);
            return string.Format(@"{0}L", value);
        }
    }
}