namespace Library.Extensions
{
    using System;

    /// <summary>
    /// The guid extensions.
    /// </summary>
    internal static class GuidExtensions
    {
        /// <summary>
        /// The to o data string.
        /// </summary>
        /// <param name="guid">
        /// The guid.
        /// </param>
        /// <param name="formattingStyle">
        /// The formatting style.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ToODataString(this Guid guid, ValueFormatter.FormattingStyle formattingStyle)
        {
            var value = guid.ToString();
            return string.Format(@"guid'{0}'", value);
        }
    }
}