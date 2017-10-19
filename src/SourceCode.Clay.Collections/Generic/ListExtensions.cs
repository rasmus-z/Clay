#region License

// Copyright (c) K2 Workflow (SourceCode Technology Holdings Inc.). All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

#endregion

using System.Collections.Generic;

namespace SourceCode.Clay.Collections.Generic
{
    /// <summary>
    /// Represents extensions for <see cref="IList{T}"/> and <see cref="IReadOnlyList{T}"/>.
    /// </summary>
    public static class ListExtensions
    {
        #region Methods

        /// <summary>
        /// Performs an optimized item-by-item comparison, using a custom <see cref="IEqualityComparer{T}"/>.
        /// The lists are required to have corresponding items in the same ordinal position.
        /// </summary>
        /// <typeparam name="T">The type of items.</typeparam>
        /// <param name="x">List 1</param>
        /// <param name="y">List 2</param>
        /// <param name="comparer">The comparer to use to test for equality.</param>
        /// <returns></returns>
        public static bool NullableListEquals<T>(this IList<T> x, IEnumerable<T> y, IEqualityComparer<T> comparer)
        {
            if (x is null ^ y is null) return false; // (x, null) or (null, y)
            if (x is null) return true; // (null, null)
            if (ReferenceEquals(x, y)) return true; // (x, x)

            // ICollection is more likely
            var isCollection = false;
            if (y is ICollection<T> yc)
            {
                if (x.Count != yc.Count) return false; // (n, m)
                if (x.Count == 0) return true; // (0, 0)

                isCollection = true;
            }
            // IReadOnlyCollection
            else if (y is IReadOnlyCollection<T> yrc)
            {
                if (x.Count != yrc.Count) return false; // (n, m)
                if (x.Count == 0) return true; // (0, 0)

                isCollection = true;
            }

            var cmpr = comparer ?? EqualityComparer<T>.Default;

            if (isCollection)
            {
                // IList is more likely
                if (y is IList<T> yl)
                {
                    // Check items in sequential order
                    for (var i = 0; i < x.Count; i++)
                    {
                        if (!cmpr.Equals(x[i], yl[i])) return false;
                    }

                    return true;
                }

                // IReadOnlyList
                if (y is IReadOnlyList<T> yrl)
                {
                    // Check items in sequential order
                    for (var i = 0; i < x.Count; i++)
                    {
                        if (!cmpr.Equals(x[i], yrl[i])) return false;
                    }

                    return true;
                }
            }

            // IEnumerable
            var equal = EnumerableExtensions.CheckEnumerable(x, y, cmpr);
            return equal;
        }

        /// <summary>
        /// Performs an optimized item-by-item comparison, using the default comparer for the type.
        /// The lists are required to have corresponding items in the same ordinal position.
        /// </summary>
        /// <typeparam name="T">The type of items.</typeparam>
        /// <param name="x">List 1</param>
        /// <param name="y">List 2</param>
        /// <returns></returns>
        public static bool NullableListEquals<T>(this IList<T> x, IEnumerable<T> y)
            => NullableListEquals(x, y, null);

        /// <summary>
        /// Performs an optimized item-by-item comparison, using a custom <see cref="IEqualityComparer{T}"/>.
        /// The lists are required to have corresponding items in the same ordinal position.
        /// </summary>
        /// <typeparam name="T">The type of items.</typeparam>
        /// <param name="x">List 1</param>
        /// <param name="y">List 2</param>
        /// <param name="comparer">The comparer to use to test for equality.</param>
        /// <returns></returns>
        public static bool NullableListEquals<T>(this IReadOnlyList<T> x, IEnumerable<T> y, IEqualityComparer<T> comparer)
        {
            if (x is null ^ y is null) return false; // (x, null) or (null, y)
            if (x is null) return true; // (null, null)
            if (ReferenceEquals(x, y)) return true; // (x, x)

            // IReadOnlyCollection is more likely
            var isCollection = false;
            if (y is IReadOnlyCollection<T> yc)
            {
                if (x.Count != yc.Count) return false; // (n, m)
                if (x.Count == 0) return true; // (0, 0)

                isCollection = true;
            }
            // ICollection
            else if (y is ICollection<T> yrc)
            {
                if (x.Count != yrc.Count) return false; // (n, m)
                if (x.Count == 0) return true; // (0, 0)

                isCollection = true;
            }

            var cmpr = comparer ?? EqualityComparer<T>.Default;

            if (isCollection)
            {
                // IReadOnlyList is more likely
                if (y is IReadOnlyList<T> yrl)
                {
                    // Check items in sequential order
                    for (var i = 0; i < x.Count; i++)
                    {
                        if (!cmpr.Equals(x[i], yrl[i])) return false;
                    }

                    return true;
                }

                // IList
                if (y is IList<T> yl)
                {
                    // Check items in sequential order
                    for (var i = 0; i < x.Count; i++)
                    {
                        if (!cmpr.Equals(x[i], yl[i])) return false;
                    }

                    return true;
                }
            }

            // IEnumerable
            var equal = EnumerableExtensions.CheckEnumerable(x, y, cmpr);
            return equal;
        }

        /// <summary>
        /// Performs an optimized item-by-item comparison, using the default comparer for the type.
        /// The lists are required to have corresponding items in the same ordinal position.
        /// </summary>
        /// <typeparam name="T">The type of items.</typeparam>
        /// <param name="x">List 1</param>
        /// <param name="y">List 2</param>
        /// <returns></returns>
        public static bool NullableListEquals<T>(this IReadOnlyList<T> x, IEnumerable<T> y)
            => NullableListEquals(x, y, null);

        #endregion
    }
}
