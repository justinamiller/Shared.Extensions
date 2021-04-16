using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Extensions
{
    public static class CharExtensions
    {
        /// <summary>
        /// Indicates whether a character is a printable character or not.
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool IsPrintable(this char c)
        {
            return c >= 32 && c <= 126;
        }

        /// <summary>
        ///  Indicates whether the specified character is categorized as a control character.
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool IsControl(this char c)
        {
            return c <= 31 || (c >= 127 && 159 >= c);
        }
    }
}
