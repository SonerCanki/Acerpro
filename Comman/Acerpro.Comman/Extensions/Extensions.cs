using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;

namespace Acerpro.Common.Extensions
{
    public static class Extensions
    {
        public static bool IsValidEmailAddress(this string s)
        {
            try
            {
                var temp = new MailAddress(s);

                var validEmailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" +
                                        @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" +
                                        @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
                var regex = new Regex(validEmailPattern, RegexOptions.IgnoreCase);

                if (!regex.IsMatch(s)) return false;
            }
            catch
            {
                return false;
            }

            return true;
        }

        public static long ToUnixTime(this DateTime date)
        {
            long unixTimeStamp = date.Ticks - new DateTime(1970, 1, 1).Ticks;
            unixTimeStamp /= TimeSpan.TicksPerSecond;
            return unixTimeStamp;
        }

        public static string Encrypt(this string str)
        {
            var bytes = Encoding.UTF8.GetBytes(str);
            var encodeStr = UrlBase64.Encode(bytes);

            if (!string.IsNullOrEmpty(encodeStr))
            {
                return encodeStr;
            }
            return str;
        }

        public static string Decrypt(this string str)
        {
            var decoded = UrlBase64.Decode(str);

            var decrypted = Encoding.UTF8.GetString(decoded);

            if (!string.IsNullOrEmpty(decrypted))
            {
                return decrypted;
            }
            return str;
        }

        public static bool IsNotNullOrWhiteSpace(this string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

        public static string GetDisplayValue<T>(this T value)
        {
            try
            {
                var fieldInfo = value.GetType().GetField(value.ToString());

                var descriptionAttributes = fieldInfo.GetCustomAttributes(
                    typeof(DisplayAttribute), false) as DisplayAttribute[];

                if (descriptionAttributes[0].ResourceType != null)
                    return LookupResource(descriptionAttributes[0].ResourceType, descriptionAttributes[0].Name);

                if (descriptionAttributes == null) return string.Empty;
                return descriptionAttributes.Length > 0 ? descriptionAttributes[0].Name : value.ToString();
            }
            catch (Exception ex)
            {
                return "TANIMSIZ";
            }
        }

        private static string LookupResource(Type resourceManagerProvider, string resourceKey)
        {
            foreach (var staticProperty in resourceManagerProvider.GetProperties(BindingFlags.Static |
                BindingFlags.NonPublic | BindingFlags.Public))
                if (staticProperty.PropertyType == typeof(ResourceManager))
                {
                    var resourceManager = (ResourceManager)staticProperty.GetValue(null, null);
                    return resourceManager.GetString(resourceKey);
                }

            return resourceKey; 
        }
    }

    public static class UrlBase64
    {
        private static readonly char[] TwoPads = { '=', '=' };

        public static string Encode(byte[] bytes, PaddingPolicy padding = PaddingPolicy.Discard)
        {
            var encoded = Convert.ToBase64String(bytes).Replace('+', '-').Replace('/', '_');
            if (padding == PaddingPolicy.Discard)
            {
                encoded = encoded.TrimEnd('=');
            }

            return encoded;
        }

        public static byte[] Decode(string encoded)
        {
            var chars = new List<char>(encoded.ToCharArray());
            for (int i = 0; i < chars.Count; i++)
            {
                if (chars[i] == '_')
                {
                    chars[i] = '/';
                }
                else if (chars[i] == '-')
                {
                    chars[i] = '+';
                }
            }

            switch (encoded.Length % 4)
            {
                case 2:
                    chars.AddRange(TwoPads);
                    break;
                case 3:
                    chars.Add('=');
                    break;
                default:
                    break;
            }

            var array = chars.ToArray();

            return Convert.FromBase64CharArray(array, 0, array.Length);
        }

        public enum PaddingPolicy
        {
            Discard,
            Preserve
        }
    }
}
