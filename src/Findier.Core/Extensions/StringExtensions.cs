using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Text.RegularExpressions;
using Findier.Core.Common;
using Newtonsoft.Json;

namespace Findier.Core.Extensions
{
    public static class StringExtensions
    {
        private static readonly Regex EmailRegex =
            new Regex(
                "^((([a-z]|\\d|[!#\\$%&'\\*\\+\\-\\/=\\?\\^_`{\\|}~]|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])+(\\.([a-z]|\\d|[!#\\$%&'\\*\\+\\-\\/=\\?\\^_`{\\|}~]|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])+)*)|((\\x22)((((\\x20|\\x09)*(\\x0d\\x0a))?(\\x20|\\x09)+)?(([\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x7f]|\\x21|[\\x23-\\x5b]|[\\x5d-\\x7e]|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])|(\\\\([\\x01-\\x09\\x0b\\x0c\\x0d-\\x7f]|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF]))))*(((\\x20|\\x09)*(\\x0d\\x0a))?(\\x20|\\x09)+)?(\\x22)))@((([a-z]|\\d|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])|(([a-z]|\\d|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])([a-z]|\\d|-|\\.|_|~|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])*([a-z]|\\d|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])))\\.)+(([a-z]|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])|(([a-z]|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])([a-z]|\\d|-|\\.|_|~|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])*([a-z]|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])))\\.?$");

        private static readonly Regex PhoneRegex =
            new Regex(
                "^(\\+\\s?)?((?<!\\+.*)\\(\\+?\\d+([\\s\\-\\.]?\\d+)?\\)|\\d+)([\\s\\-\\.]?(\\(\\d+([\\s\\-\\.]?\\d+)?\\)|\\d+))*(\\s?(x|ext\\.?)\\s?\\d+)?$");

        public static string Append(this string left, string right) => left + " " + right;

        /// <summary>
        ///     DeTokenizes the specified values.
        ///     "test yo%20testing" =&gt; ["test", "yo testing"]
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public static string[] DeTokenize(this string token)
        {
            // reverse the proccess of encoding
            var values = token.Split(' ');
            return values.Select(p => p?.Replace("%20", " ").Replace("%25", "%")).ToArray();
        }

        public static bool EndsWithIgnoreCase(this string text, string other) =>
            text?.EndsWith(other, StringComparison.CurrentCultureIgnoreCase) ?? false;

        public static bool EqualsIgnoreCase(this string text, string other) =>
            text?.Equals(other, StringComparison.CurrentCultureIgnoreCase) ?? false;

        public static string GetEnumText(this Enum value)
        {
            var fi = value.GetType().GetRuntimeField(value.ToString());

            var attributes =
                (TextAttribute[])fi.GetCustomAttributes(typeof (TextAttribute), false);

            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0].Text;
            }
            return value.ToString();
        }

        public static bool IsAnyNullOrEmpty(params string[] values) => values.Any(string.IsNullOrEmpty);

        public static bool IsEmail(this string str) => EmailRegex.IsMatch(str);

        public static bool IsPhoneNumber(this string str) => PhoneRegex.IsMatch(str);

        public static string SerializeToJson(this object obj)
        {
            try
            {
                return JsonConvert.SerializeObject(obj);
            }
            catch
            {
                return null;
            }
        }

        public static string SerializeToJsonWithTypeInfo(this object obj)
        {
            try
            {
                return JsonConvert.SerializeObject(obj,
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All,
                        TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple
                    });
            }
            catch
            {
                return null;
            }
        }

        public static string ToBase64(this byte[] bytes) => Convert.ToBase64String(bytes);

        public static string ToBase64(this string text) => Convert.ToBase64String(text.ToBytes());

        public static byte[] ToBytes(this string text) => Encoding.UTF8.GetBytes(text);

        public static string ToHex(this byte[] bytes) => BitConverter.ToString(bytes).Replace("-", "").ToLower();

        public static string ToHtmlStrippedText(this string str)
        {
            var array = new char[str.Length];
            var arrayIndex = 0;
            var inside = false;

            foreach (var o in str.ToCharArray())
            {
                switch (o)
                {
                    case '<':
                        inside = true;
                        continue;
                    case '>':
                        inside = false;
                        continue;
                }
                if (inside)
                {
                    continue;
                }

                array[arrayIndex] = o;
                arrayIndex++;
            }
            return new string(array, 0, arrayIndex);
        }

        /// <summary>
        ///     Tokenizes the specified values.
        ///     Simple array serializing.
        ///     ["test", "yo testing"] =&gt; "test yo%20testing"
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns></returns>
        public static string Tokenize(this string[] values)
        {
            // the delimiter used for tokenizing is a space, let's encode it.
            // Encode % and then we can encode space, without worring if the original string had %.
            var encoded = values.Select(p => p?.Replace("%", "%25").Replace(" ", "%20"));

            // Join using the space delimiter
            return string.Join(" ", encoded);
        }

        public static string ToSanitizedFileName(this string str, string invalidMessage = null)
        {
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }

            if (str.Length > 35)
            {
                str = str.Substring(0, 35);
            }

            str = str.ToValidFileNameEnding();

            /*
             * A filename cannot contain any of the following characters:
             * \ / : * ? " < > |
             */
            var name =
                str.Replace("\\", string.Empty)
                    .Replace("/", string.Empty)
                    .Replace(":", " ")
                    .Replace("*", string.Empty)
                    .Replace("?", string.Empty)
                    .Replace("\"", "'")
                    .Replace("<", string.Empty)
                    .Replace(">", string.Empty)
                    .Replace("|", " ");

            return string.IsNullOrEmpty(name) ? invalidMessage ?? "Invalid" : name;
        }

        public static string ToSentenceCase(this string str)
        {
            return Regex.Replace(str, "[a-z][A-Z]", m => m.Value[0] + " " + char.ToLower(m.Value[1]));
        }

        public static Uri ToUri(this string url, UriKind kind = UriKind.Absolute)
        {
            return new Uri(url, kind);
        }

        public static string ToValidFileNameEnding(this string str)
        {
            var isNonAccepted = true;

            while (isNonAccepted)
            {
                var lastChar = str[str.Length - 1];

                isNonAccepted = lastChar == ' ' || lastChar == '.' || lastChar == ';' || lastChar == ':';

                if (isNonAccepted)
                {
                    str = str.Remove(str.Length - 1);
                }
                else
                {
                    break;
                }

                if (str.Length == 0)
                {
                    return str;
                }

                isNonAccepted = lastChar == ' ' || lastChar == '.' || lastChar == ';' || lastChar == ':';
            }

            return str;
        }

        public static T TryDeserializeJson<T>(this string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch
            {
                return default(T);
            }
        }

        public static object TryDeserializeJsonWithTypeInfo(this string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                return json;
            }

            try
            {
                return JsonConvert.DeserializeObject(json,
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All,
                        TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple
                    });
            }
            catch
            {
                return null;
            }
        }
    }
}