using System;
using System.Collections.Generic;
using System.Text;

namespace Dotnet.Extensions
{
    public static class ArrayExtensions
    {
        public static T[] ArrayEmpty<T>()
        {
            // Enumerable.Empty<T> no longer returns an empty array in .NET Core 3.0
            return EmptyArrayContainer<T>.Empty;
        }

        private static class EmptyArrayContainer<T>
        {
#pragma warning disable CA1825 // Avoid zero-length array allocations.
#pragma warning disable HAA0501 // Explicit new array type allocation
            public static readonly T[] Empty = new T[0];
#pragma warning restore HAA0501 // Explicit new array type allocation
#pragma warning restore CA1825 // Avoid zero-length array allocations.
        }

        /// <summary>
        /// Checks if two arrays have matching data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="test1"></param>
        /// <param name="test2"></param>
        /// <returns></returns>
        public static bool SequenceMatch<T>(this T[] test1, T[] test2)
        {
            bool result = false;
            if (test1 != null && test2 != null)
            {
                int test1Len = test1.Length;
                if (test1Len == test2.Length)
                {
                    int matchCount = 0;
                    for (int i = 0; i < test1Len; i++)
                    {
                        if (!test1[i].Equals(test2[i]))
                        {
                            break;
                        }

                        matchCount++;
                    }

                    result = matchCount == test1Len;
                }//end length equal check
            }

            return result;
        }
    }
}
