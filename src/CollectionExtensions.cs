using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Extensions
{
    public static class CollectionExtensions
    {
        /// <summary>
        /// Determines whether the collection is <c>null</c> or empty.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this ICollection<T> collection)
        {
            if (collection != null)
            {
                return collection.Count == 0;
            }
            return true;
        }


        /// <summary>
        ///  Reverse order List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void FastReverse<T>(this IList<T> list)
        {
            int i = 0;
            int j = list.Count - 1;
            while (i < j)
            {
                T temp = list[i];
                list[i] = list[j];
                list[j] = temp;
                i++;
                j--;
            }
        }
    }
}
