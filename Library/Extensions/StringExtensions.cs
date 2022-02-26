namespace Library.Extensions
{
    using System;
    using System.Linq;

    /// <summary>
    /// The string extensions.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// The is plural.
        /// </summary>
        /// <param name="str">
        /// The str.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsPlural(this string str)
        {
            return _pluralizer.IsPlural(str);
        }

        /// <summary>
        /// The pluralize.
        /// </summary>
        /// <param name="str">
        /// The str.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string Pluralize(this string str)
        {
            return _pluralizer.Pluralize(str);
        }

        /// <summary>
        /// The singularize.
        /// </summary>
        /// <param name="str">
        /// The str.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string Singularize(this string str)
        {
            return _pluralizer.Singularize(str);
        }

        /// <summary>
        /// The is all upper case.
        /// </summary>
        /// <param name="str">
        /// The str.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsAllUpperCase(this string str)
        {
            return !str.Cast<char>().Any(char.IsLower);
        }

        /// <summary>
        /// The null if whitespace.
        /// </summary>
        /// <param name="str">
        /// The str.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string NullIfWhitespace(this string str)
        {
            return string.IsNullOrWhiteSpace(str) ? null : str;
        }

        /// <summary>
        /// The or default.
        /// </summary>
        /// <param name="str">
        /// The str.
        /// </param>
        /// <param name="defaultValue">
        /// The default value.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string OrDefault(this string str, string defaultValue)
        {
            return str ?? defaultValue;
        }

        /// <summary>
        /// The _pluralizer.
        /// </summary>
        private static IPluralizer _pluralizer = new SimplePluralizer();

        /// <summary>
        /// The set pluralizer.
        /// </summary>
        /// <param name="pluralizer">
        /// The pluralizer.
        /// </param>
        internal static void SetPluralizer(IPluralizer pluralizer)
        {
            _pluralizer = pluralizer ?? new SimplePluralizer();
        }

        /// <summary>
        /// The ensure starts with.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string EnsureStartsWith(this string source, string value)
        {
            return (source == null || source.StartsWith(value)) ? source : value + source;
        }

        /// <summary>
        /// The normalize url.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string NormalizeUrl(this string url)
        {
            if (url.EndsWith("/"))
            {
                return url.Substring(0, url.Length - 1);
            }

            return url;
        }
    }

    /// <summary>
    /// The simple pluralizer.
    /// </summary>
    internal class SimplePluralizer : IPluralizer
    {
        /// <summary>
        /// The is singular.
        /// </summary>
        /// <param name="word">
        /// The word.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsSingular(string word)
        {
            return !this.IsPlural(word);
        }

        /// <summary>
        /// The is plural.
        /// </summary>
        /// <param name="word">
        /// The word.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsPlural(string word)
        {
            return word.EndsWith("s", StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// The pluralize.
        /// </summary>
        /// <param name="word">
        /// The word.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string Pluralize(string word)
        {
            if (word.EndsWith("y", StringComparison.OrdinalIgnoreCase))
            {
                word = word.Substring(0, word.Length - 1) + (word.IsAllUpperCase() ? "IE" : "ie");
            }

            return string.Concat(word, word.IsAllUpperCase() ? "S" : "s");
        }

        /// <summary>
        /// The singularize.
        /// </summary>
        /// <param name="word">
        /// The word.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string Singularize(string word)
        {
            return word.EndsWith("s", StringComparison.OrdinalIgnoreCase) ? word.Substring(0, word.Length - 1) : word;
        }
    }

    internal interface IPluralizer
    {
        /// <summary>
        /// The is singular.
        /// </summary>
        /// <param name="word">
        /// The word.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool IsSingular(string word);

        /// <summary>
        /// The is plural.
        /// </summary>
        /// <param name="word">
        /// The word.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool IsPlural(string word);

        /// <summary>
        /// The pluralize.
        /// </summary>
        /// <param name="word">
        /// The word.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string Pluralize(string word);

        /// <summary>
        /// The singularize.
        /// </summary>
        /// <param name="word">
        /// The word.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string Singularize(string word);
    }
}