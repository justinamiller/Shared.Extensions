using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Extensions
{
    /// <summary>
    /// Provides a cached reusable instance of a StringBuilder per thread.
    /// </summary>
    ///<remarks>
    ///will reduce overall GC for small strings
    ///</remarks>
    public static class StringBuilderPool
    {
        //Avoid stringbuilder block fragmentation within a thread context.
        private const int MAX_BUILDER_SIZE = 360;
        //default capacity // == StringBuilder.DefaultCapacity
        private const int DEFAULT_CAPACITY = 16;

        [ThreadStatic]
        private static StringBuilder s_cachedInstance;

        /// <summary>
        /// Gets a string builder to use of a particular size.
        /// </summary>
        public static StringBuilder Get(int capacity = DEFAULT_CAPACITY)
        {
            if (capacity <= MAX_BUILDER_SIZE)
            {
                StringBuilder sb = s_cachedInstance;
                if (sb != null)
                {
                    // Avoid stringbuilder block fragmentation by getting a new StringBuilder
                    // when the requested size is larger than the current capacity
                    if (capacity <= sb.Capacity)
                    {
                        s_cachedInstance = null;
                        //clear buffer
                        sb.Clear();
                        return sb;
                    }
                }
            }
            return new StringBuilder(capacity);
        }

        /// <summary>
        /// Place the specified builder in the cache if it is not too big.
        /// </summary>
        public static void Release(this StringBuilder sb)
        {
            if (sb?.Capacity <= MAX_BUILDER_SIZE)
            {
                s_cachedInstance = sb;
            }
        }

        /// <summary>
        /// Gets the resulting string and releases a StringBuilder instance.
        /// </summary>
        public static string GetStringAndRelease(this StringBuilder sb)
        {
            string result = sb?.ToString();
            Release(sb);
            return result;
        }
    }
}
