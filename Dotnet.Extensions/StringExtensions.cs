using System;
using System.Globalization;

namespace Dotnet.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        ///  Indicates whether the specified string is null or an Empty string.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string value)
        {
            return value == null || value.Length == 0;
        }

        /// <summary>
        ///   Indicates whether a specified string is null, empty, or consists only of white-space characters.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string value)
        {
            if (value == null)
            {
                return true;
            }

            for(int i = 0; i < value.Length; i++)
            {
                if (!char.IsWhiteSpace(value[i]))
                {
                    return false;
                }
            }

            return true;
        }


        /// <summary>
        /// Splitting a string into chunks of a certain size
        /// </summary>
        /// <param name="str"></param>
        /// <param name="chunkLength"></param>
        /// <returns></returns>
        public static string[] SplitBy(this string str, int chunkLength)
        {
            if (!str.IsNullOrEmpty())
            {
                if (chunkLength >= 1)
                {
                    int strLength = str.Length;

                    string[] arrayStrings = new string[strLength / chunkLength + 1];

                    int size = arrayStrings.Length;
                    int index = 0;
                    for (int i = 0; i < strLength; i += chunkLength)
                    {
                        if (chunkLength + i > strLength)
                        {
                            arrayStrings[index] = str.Substring(i, strLength - i);
                            index++;
                        }
                        else
                        {
                            arrayStrings[index] = str.Substring(i, chunkLength);
                            index++;
                        }

                        if (index >= size)
                        {
                            //need to grow
                            Array.Resize(ref arrayStrings, size + 5);
                            size = arrayStrings.Length;
                        }
                    }

                    if (index != size)
                    {
                        //need to shirnk.
                        Array.Resize(ref arrayStrings, index);
                    }

                    return arrayStrings;
                }
                else
                {
                    //neg #
                    return new string[1] { str };
                }
            }

            //empty array
            return new string[0];
        }

        /// <summary>
        /// Trim length of string
        /// </summary>
        /// <param name="value"></param>
        /// <param name="limitSize"></param>
        /// <returns></returns>
        public static string TrimLength(this string value, int limitSize)
        {
            if (value != null && limitSize > 0)
            {
                if (value.Length > limitSize)
                {
                    return value.Substring(0, limitSize);
                }
            }
            return value;
        }


        /// <summary>
        /// Naming convention in which each word within a compound word is capitalized except for the first word
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToCamelCase(this string value)
        {
            if (value.IsNullOrWhiteSpace() || !char.IsUpper(value[0]))
            {
                return value;
            }

            char[] chars = value.ToCharArray();

            for (int i = 0; i < chars.Length; i++)
            {
                if (i == 1 && !char.IsUpper(chars[i]))
                {
                    break;
                }

                bool hasNext = (i + 1 < chars.Length);
                if (i > 0 && hasNext && !char.IsUpper(chars[i + 1]))
                {
                    if (char.IsSeparator(chars[i + 1]))
                    {
                        chars[i] = char.ToLower(chars[i], CultureInfo.InvariantCulture); 
                    }

                    break;
                }

                chars[i] = char.ToLower(chars[i], CultureInfo.InvariantCulture);
            }

            return new string(chars);
        }

        public static bool IsBoolean(this string input)
        {
          return  bool.TryParse(input, out _); // Discards
        }


        public static bool IsBoolean(this string input, out bool result)
        {
            return bool.TryParse(input, out result);
        }

        public static bool IsDateTime(this string input)
        {
            return DateTime.TryParse(input, out _); //Discards;
        }

        public static bool IsDateTime(this string input, out DateTime result)
        {
            return DateTime.TryParse(input, out result);
        }

        public static bool IsDateTime(this string input, string format)
        {
            return DateTime.TryParseExact(input, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out _); //Discards
        }

        public static bool IsDateTime(this string input, string format, out DateTime result)
        {
            return DateTime.TryParseExact(input, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out result);
        }

        public static bool IsDouble(this string input)
        {
           return  double.TryParse(input, out _); //Discards
        }

        public static bool IsDouble(this string input, out double result)
        {
           return  double.TryParse(input, out result);
        }

        public static bool IsInt(this string input)
        {
            return int.TryParse(input, out _); //Discards
        }


        public static bool IsInt(this string input, out int result)
        {
            return int.TryParse(input, out result);
        }

        public static bool IsLong(this string input)
        {
            return long.TryParse(input, out _); //Discards
        }


        public static bool IsLong(this string input, out long result)
        {
            return long.TryParse(input, out result);
        }
    }
}
