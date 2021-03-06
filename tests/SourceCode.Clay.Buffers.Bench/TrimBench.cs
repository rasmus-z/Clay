#region License

// Copyright (c) K2 Workflow (SourceCode Technology Holdings Inc.). All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

#endregion

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;

namespace SourceCode.Clay.Buffers.Bench
{
    /*
    */

    //[MemoryDiagnoser]
    //[RankColumn]
    //[Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
    public class TrimBench
    {
        private const int _iterations = 10_000;
        private const int N = 100;

        public byte[] Data = new byte[]
        {
            10, 10, 10, 10, 10, 9, 10, 8,
            123, 231, 124, 241, 125, 251, 126, 162,
            9, 10, 7, 10, 8, 10, 10, 10
        };

        // Passing style

        //[Benchmark(Baseline = true, OperationsPerInvoke = _iterations * N)]
        //public long SingleViaNone()
        //{
        //    long sum = 0;

        //    Span<byte> span = Data.Span;
        //    for (uint n = 0; n < N; n++)
        //    {
        //        sum += TrimViaNone(span, (byte)10).Length;
        //    }

        //    return sum;
        //}

        //[Benchmark(Baseline = false, OperationsPerInvoke = _iterations * N)]
        //public long SingleViaIn()
        //{
        //    long sum = 0;

        //    Span<byte> span = Data.Span;
        //    for (uint n = 0; n < N; n++)
        //    {
        //        sum += TrimViaIn(span, (byte)10).Length;
        //    }

        //    return sum;
        //}

        //[Benchmark(Baseline = false, OperationsPerInvoke = _iterations * N)]
        //public long SingleViaRef()
        //{
        //    long sum = 0;

        //    Span<byte> span = Data.Span;
        //    for (uint n = 0; n < N; n++)
        //    {
        //        sum += TrimViaRef(span, (byte)10).Length;
        //    }

        //    return sum;
        //}

        // Guard clauses

        //[Benchmark(Baseline = true, OperationsPerInvoke = _iterations * N)]
        //public long SingleNoGuard()
        //{
        //    long sum = 0;

        //    Span<byte> span = Data.Span;
        //    for (uint n = 0; n < N; n++)
        //    {
        //        sum += TrimNoGuard<byte>(span, Array.Empty<byte>()).Length;
        //        sum += TrimNoGuard<byte>(span, new byte[] { 10 }).Length;
        //        sum += TrimNoGuard<byte>(span, new byte[] { 10, 9, 8 }).Length;
        //    }

        //    return sum;
        //}

        //[Benchmark(Baseline = false, OperationsPerInvoke = _iterations * N)]
        //public long SingleViaSwitch()
        //{
        //    long sum = 0;

        //    Span<byte> span = Data.Span;
        //    for (uint n = 0; n < N; n++)
        //    {
        //        sum += TrimViaSwitch<byte>(span, Array.Empty<byte>()).Length;
        //        sum += TrimViaSwitch<byte>(span, new byte[] { 10 }).Length;
        //        sum += TrimViaSwitch<byte>(span, new byte[] { 10, 9, 8 }).Length;
        //    }

        //    return sum;
        //}

        //[Benchmark(Baseline = false, OperationsPerInvoke = _iterations * N)]
        //public long SingleViaIf()
        //{
        //    long sum = 0;

        //    Span<byte> span = Data.Span;
        //    for (uint n = 0; n < N; n++)
        //    {
        //        sum += TrimViaIf<byte>(span, Array.Empty<byte>()).Length;
        //        sum += TrimViaIf<byte>(span, new byte[] { 10 }).Length;
        //        sum += TrimViaIf<byte>(span, new byte[] { 10, 9, 8 }).Length;
        //    }

        //    return sum;
        //}

        //[Benchmark(Baseline = false, OperationsPerInvoke = _iterations * N)]
        //public long SingleViaNestedIf()
        //{
        //    long sum = 0;

        //    Span<byte> span = Data.Span;
        //    for (uint n = 0; n < N; n++)
        //    {
        //        sum += TrimViaNestedIf<byte>(span, Array.Empty<byte>()).Length;
        //        sum += TrimViaNestedIf<byte>(span, new byte[] { 10 }).Length;
        //        sum += TrimViaNestedIf<byte>(span, new byte[] { 10, 9, 8 }).Length;
        //    }

        //    return sum;
        //}

        // Marshall

        //[Benchmark(Baseline = true, OperationsPerInvoke = _iterations * N)]
        //public long TrimRoM()
        //{
        //    long sum = 0;

        //    var rom = new ReadOnlyMemory<byte>(Data);
        //    for (uint n = 0; n < N; n++)
        //    {
        //        sum += Trim<byte>(rom, 10).Length;
        //        sum += Trim<byte>(rom, 9).Length;
        //        sum += Trim<byte>(rom, 8).Length;
        //    }

        //    return sum;
        //}

        //[Benchmark(Baseline = false, OperationsPerInvoke = _iterations * N)]
        //public long TrimMemory()
        //{
        //    long sum = 0;

        //    var mem = new Memory<byte>(Data);
        //    for (uint n = 0; n < N; n++)
        //    {
        //        sum += Trim<byte>(mem, 10).Length;
        //        sum += Trim<byte>(mem, 9).Length;
        //        sum += Trim<byte>(mem, 8).Length;
        //    }

        //    return sum;
        //}

        //[Benchmark(Baseline = false, OperationsPerInvoke = _iterations * N)]
        //public long TrimMemoryMarshall()
        //{
        //    long sum = 0;

        //    var mem = new Memory<byte>(Data);
        //    for (uint n = 0; n < N; n++)
        //    {
        //        sum += TrimMarshal<byte>(mem, 10).Length;
        //        sum += TrimMarshal<byte>(mem, 9).Length;
        //        sum += TrimMarshal<byte>(mem, 8).Length;
        //    }

        //    return sum;
        //}


        // IndexOfAny

        //[Benchmark(Baseline = true, OperationsPerInvoke = _iterations * N)]
        //public long ViaSequential()
        //{
        //    long sum = 0;

        //    var span = new ReadOnlySpan<byte>(Data);
        //    for (uint n = 0; n < N; n++)
        //    {
        //        sum += Trim(span, Array.Empty<byte>()).Length;
        //        sum += Trim(span, new byte[] { 10 }).Length;
        //        sum += Trim(span, new byte[] { 10, 9, 8 }).Length;
        //    }

        //    return sum;
        //}

        //[Benchmark(Baseline = false, OperationsPerInvoke = _iterations * N)]
        //public long ViaIndexOfAny()
        //{
        //    long sum = 0;

        //    var span = new ReadOnlySpan<byte>(Data);
        //    for (uint n = 0; n < N; n++)
        //    {
        //        sum += TrimIndexOfAny(span, Array.Empty<byte>()).Length;
        //        sum += TrimIndexOfAny(span, new byte[] { 10 }).Length;
        //        sum += TrimIndexOfAny(span, new byte[] { 10, 9, 8 }).Length;
        //    }

        //    return sum;
        //}

        //[Benchmark(Baseline = false, OperationsPerInvoke = _iterations * N)]
        //public long ViaContains()
        //{
        //    long sum = 0;

        //    var span = new ReadOnlySpan<byte>(Data);
        //    for (uint n = 0; n < N; n++)
        //    {
        //        sum += TrimContains(span, Array.Empty<byte>()).Length;
        //        sum += TrimContains(span, new byte[] { 10 }).Length;
        //        sum += TrimContains(span, new byte[] { 10, 9, 8 }).Length;
        //    }

        //    return sum;
        //}

        // ClampEnd local function

        //[Benchmark(Baseline = true, OperationsPerInvoke = _iterations * N)]
        //public long ClampEndLocal()
        //{
        //    long sum = 0;

        //    var span = new ReadOnlySpan<byte>(Data);
        //    for (uint n = 0; n < N; n++)
        //    {
        //        sum += ClampEndLocal<byte>(span, 1, 10);
        //        sum += ClampEndLocal<byte>(span, 1, 9);
        //        sum += ClampEndLocal<byte>(span, 1, 8);
        //    }

        //    return sum;
        //}

        //[Benchmark(Baseline = false, OperationsPerInvoke = _iterations * N)]
        //public long ClampEndDefault()
        //{
        //    long sum = 0;

        //    var span = new ReadOnlySpan<byte>(Data);
        //    for (uint n = 0; n < N; n++)
        //    {
        //        sum += ClampEndDefault<byte>(span, 1, 10);
        //        sum += ClampEndDefault<byte>(span, 1, 9);
        //        sum += ClampEndDefault<byte>(span, 1, 8);
        //    }

        //    return sum;
        //}

        //private static int ClampEndLocal<T>(ReadOnlySpan<T> span, int start, T trimElement)
        //    where T : IEquatable<T>
        //{
        //    // Initially, start==len==0. If ClampStart trims all, start==len
        //    Debug.Assert((uint)start <= span.Length);

        //    int end = span.Length - 1;

        //    if (trimElement != null)
        //    {
        //        for (; end >= start; end--)
        //        {
        //            if (!trimElement.Equals(span[end]))
        //            {
        //                break;
        //            }
        //        }

        //        return end - start + 1;
        //    }

        //    return ClampNull(span, start, end);

        //    int ClampNull(ReadOnlySpan<T> span1, int start1, int end1)
        //    {
        //        for (; end1 >= start1; end1--)
        //        {
        //            if (span1[end1] != null)
        //            {
        //                break;
        //            }
        //        }

        //        return end1 - start1 + 1;
        //    }
        //}

        //private static int ClampEndDefault<T>(ReadOnlySpan<T> span, int start, T trimElement)
        //    where T : IEquatable<T>
        //{
        //    // Initially, start==len==0. If ClampStart trims all, start==len
        //    Debug.Assert((uint)start <= span.Length);

        //    int end = span.Length - 1;

        //    if (trimElement != null)
        //    {
        //        for (; end >= start; end--)
        //        {
        //            if (!trimElement.Equals(span[end]))
        //            {
        //                break;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        for (; end >= start; end--)
        //        {
        //            if (span[end] != null)
        //            {
        //                break;
        //            }
        //        }
        //    }

        //    return end - start + 1;
        //}

        #region Switch


        [Benchmark(Baseline = false, OperationsPerInvoke = _iterations * N)]
        public long ClampSwitch()
        {
            long sum = 0;

            var span = new ReadOnlySpan<byte>(Data);
            for (int n = 0; n < N; n++)
            {
                ReadOnlySpan<byte> trims = new ReadOnlySpan<byte>(new byte[] { 10, 9, 8 }).Slice(0, n % 4);

                sum += span.TrimStartSwitch(trims).Length;
            }

            return sum;
        }

        [Benchmark(Baseline = true, OperationsPerInvoke = _iterations * N)]
        public long ClampIf()
        {
            long sum = 0;

            var span = new ReadOnlySpan<byte>(Data);
            for (int n = 0; n < N; n++)
            {
                ReadOnlySpan<byte> trims = new ReadOnlySpan<byte>(new byte[] { 10, 9, 8 }).Slice(0, n % 4);

                sum += span.TrimStartIf(trims).Length;
            }

            return sum;
        }

        #endregion

        #region Via None

        /// <summary>
        /// Removes all leading and trailing occurrences of a specified element.
        /// </summary>
        /// <param name="trimElement">The specified element to look for and remove.</param>
        public static ReadOnlySpan<T> Trim<T>(ReadOnlySpan<T> span, T trimElement)
            where T : IEquatable<T>
        {
            int start = ClampStart(span, trimElement);
            int length = ClampEnd(span, start, trimElement);
            return span.Slice(start, length);
        }

        /// <summary>
        /// Removes all leading occurrences of a specified element.
        /// </summary>
        /// <param name="trimElement">The specified element to look for and remove.</param>
        public static ReadOnlySpan<T> TrimStart<T>(ReadOnlySpan<T> span, T trimElement)
            where T : IEquatable<T>
        {
            int start = ClampStart(span, trimElement);
            return span.Slice(start);
        }

        /// <summary>
        /// Removes all trailing occurrences of a specified element.
        /// </summary>
        /// <param name="trimElement">The specified element to look for and remove.</param>
        public static ReadOnlySpan<T> TrimEnd<T>(ReadOnlySpan<T> span, T trimElement)
            where T : IEquatable<T>
        {
            int length = ClampEnd(span, 0, trimElement);
            return span.Slice(0, length);
        }

        /// <summary>
        /// Removes all leading and trailing occurrences of a set of elements specified
        /// in a readonly span from the span.
        /// </summary>
        /// <param name="trimElements">The span which contains the set of elements to remove.</param>
        /// <remarks>If <paramref name="trimElements"/> is empty, the span is returned unaltered.</remarks>
        public static ReadOnlySpan<T> Trim<T>(ReadOnlySpan<T> span, ReadOnlySpan<T> trimElements)
            where T : IEquatable<T>
        {
            if (trimElements.Length <= 1) // Optimize for N > 1
            {
                return trimElements.Length == 0 ? span : Trim(span, trimElements[0]);
            }

            int start = ClampStart(span, trimElements);
            int length = ClampEnd(span, start, trimElements);
            return span.Slice(start, length);
        }

        /// <summary>
        /// Removes all leading occurrences of a set of elements specified
        /// in a readonly span from the span.
        /// </summary>
        /// <param name="trimElements">The span which contains the set of elements to remove.</param>
        /// <remarks>If <paramref name="trimElements"/> is empty, the span is returned unaltered.</remarks>
        public static ReadOnlySpan<T> TrimStart<T>(ReadOnlySpan<T> span, ReadOnlySpan<T> trimElements)
            where T : IEquatable<T>
        {
            if (trimElements.Length <= 1) // Optimize for N > 1
            {
                return trimElements.Length == 0 ? span : TrimStart(span, trimElements[0]);
            }

            int start = ClampStart(span, trimElements);
            return span.Slice(start);
        }

        /// <summary>
        /// Removes all trailing occurrences of a set of elements specified
        /// in a readonly span from the span.
        /// </summary>
        /// <param name="trimElements">The span which contains the set of elements to remove.</param>
        /// <remarks>If <paramref name="trimElements"/> is empty, the span is returned unaltered.</remarks>
        public static ReadOnlySpan<T> TrimEnd<T>(ReadOnlySpan<T> span, ReadOnlySpan<T> trimElements)
            where T : IEquatable<T>
        {
            if (trimElements.Length <= 1) // Optimize for N > 1
            {
                return trimElements.Length == 0 ? span : TrimEnd(span, trimElements[0]);
            }

            int length = ClampEnd(span, 0, trimElements);
            return span.Slice(0, length);
        }

        /// <summary>
        /// Delimits all leading occurrences of a specified element.
        /// </summary>
        /// <param name="span">The source span from which the element is removed.</param>
        /// <param name="trimElement">The specified element to look for and remove.</param>
        private static int ClampStart<T>(ReadOnlySpan<T> span, T trimElement)
            where T : IEquatable<T>
        {
            int start = 0;

            if (trimElement == null)
            {
                for (; start < span.Length; start++)
                {
                    if (span[start] != null)
                        break;
                }
            }
            else
            {
                for (; start < span.Length; start++)
                {
                    if (!trimElement.Equals(span[start]))
                        break;
                }
            }

            return start;
        }

        /// <summary>
        /// Delimits all trailing occurrences of a specified element.
        /// </summary>
        /// <param name="span">The source span from which the element is removed.</param>
        /// <param name="start">The start index from which to being searching.</param>
        /// <param name="trimElement">The specified element to look for and remove.</param>
        private static int ClampEnd<T>(ReadOnlySpan<T> span, int start, T trimElement)
            where T : IEquatable<T>
        {
            // start==len==0, start<len, start==len after ClampStart trims all
            Debug.Assert((uint)start < span.Length);

            int end = span.Length - 1;

            if (trimElement == null)
            {
                for (; end >= start; end--)
                {
                    if (span[end] != null)
                        break;
                }
            }
            else
            {
                for (; end >= start; end--)
                {
                    if (!trimElement.Equals(span[start]))
                        break;
                }
            }

            return end - start + 1;
        }

        /// <summary>
        /// Delimits all leading occurrences of a specified element.
        /// </summary>
        /// <param name="span">The source span from which the element is removed.</param>
        /// <param name="trimElements">The span which contains the set of elements to remove.</param>
        private static int ClampStart<T>(ReadOnlySpan<T> span, ReadOnlySpan<T> trimElements)
            where T : IEquatable<T>
        {
            int start = 0;

            for (; start < span.Length; start++)
            {
                if (!SequentialContains(trimElements, span[start]))
                    break;
            }

            return start;
        }

        /// <summary>
        /// Delimits all trailing occurrences of a specified element.
        /// </summary>
        /// <param name="span">The source span from which the element is removed.</param>
        /// <param name="start">The start index from which to being searching.</param>
        /// <param name="trimElements">The span which contains the set of elements to remove.</param>
        private static int ClampEnd<T>(ReadOnlySpan<T> span, int start, ReadOnlySpan<T> trimElements)
            where T : IEquatable<T>
        {
            // Initially, start==len==0. If ClampStart trims all, start==len
            Debug.Assert((uint)start <= span.Length);

            int end = span.Length - 1;

            for (; end >= start; end--)
            {
                if (!SequentialContains(trimElements, span[end]))
                    break;
            }

            return end - start + 1;
        }

        /// <summary>
        /// Scans for a specified item in the provided collection, returning true if found, else false.
        /// Optimized for a small number of elements.
        /// </summary>
        /// <param name="trimElements">The span which contains the set of elements to remove.</param>
        /// <param name="item">The item to try find.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool SequentialContains<T>(ReadOnlySpan<T> trimElements, T item)
            where T : IEquatable<T>
        {
            // Non-vectorized scan optimized for small N
            for (int i = 0; i < trimElements.Length; i++)
            {
                if (trimElements[i] == null)
                {
                    if (item == null)
                        return true;
                }
                else if (trimElements[i].Equals(item))
                {
                    return true;
                }
            }

            return false;
        }

        #endregion

        #region Via In

        /// <summary>
        /// Removes all leading and trailing occurrences of a specified element.
        /// </summary>
        /// <param name="trimElement">The specified element to look for and remove.</param>
        public static ReadOnlySpan<T> TrimViaIn<T>(ReadOnlySpan<T> span, T trimElement)
            where T : IEquatable<T>
        {
            int start = ClampViaInStart(in span, trimElement);
            int length = ClampViaInEnd(in span, start, trimElement);
            return span.Slice(start, length);
        }

        /// <summary>
        /// Removes all leading occurrences of a specified element.
        /// </summary>
        /// <param name="trimElement">The specified element to look for and remove.</param>
        public static ReadOnlySpan<T> TrimViaInStart<T>(ReadOnlySpan<T> span, T trimElement)
            where T : IEquatable<T>
        {
            int start = ClampViaInStart(in span, trimElement);
            return span.Slice(start);
        }

        /// <summary>
        /// Removes all trailing occurrences of a specified element.
        /// </summary>
        /// <param name="trimElement">The specified element to look for and remove.</param>
        public static ReadOnlySpan<T> TrimViaInEnd<T>(ReadOnlySpan<T> span, T trimElement)
            where T : IEquatable<T>
        {
            int length = ClampViaInEnd(in span, 0, trimElement);
            return span.Slice(0, length);
        }

        /// <summary>
        /// Delimits all leading occurrences of a specified element.
        /// </summary>
        /// <param name="span">The source span from which the element is removed.</param>
        /// <param name="trimElement">The specified element to look for and remove.</param>
        private static int ClampViaInStart<T>(in ReadOnlySpan<T> span, in T trimElement)
            where T : IEquatable<T>
        {
            int start = 0;

            if (trimElement == null)
            {
                for (; start < span.Length; start++)
                {
                    if (span[start] != null)
                        break;
                }
            }
            else
            {
                for (; start < span.Length; start++)
                {
                    if (!trimElement.Equals(span[start]))
                        break;
                }
            }

            return start;
        }

        /// <summary>
        /// Delimits all trailing occurrences of a specified element.
        /// </summary>
        /// <param name="span">The source span from which the element is removed.</param>
        /// <param name="start">The start index from which to being searching.</param>
        /// <param name="trimElement">The specified element to look for and remove.</param>
        private static int ClampViaInEnd<T>(in ReadOnlySpan<T> span, int start, in T trimElement)
            where T : IEquatable<T>
        {
            // start==len==0, start<len, start==len after ClampStart trims all
            Debug.Assert((uint)start < span.Length);

            int end = span.Length - 1;

            if (trimElement == null)
            {
                for (; end >= start; end--)
                {
                    if (span[end] != null)
                        break;
                }
            }
            else
            {
                for (; end >= start; end--)
                {
                    if (!trimElement.Equals(span[start]))
                        break;
                }
            }

            return end - start + 1;
        }

        #endregion

        #region Via Ref

        /// <summary>
        /// Removes all leading and trailing occurrences of a specified element.
        /// </summary>
        /// <param name="trimElement">The specified element to look for and remove.</param>
        public static ReadOnlySpan<T> TrimViaRef<T>(ReadOnlySpan<T> span, T trimElement)
            where T : IEquatable<T>
        {
            int start = ClampViaRefStart(ref span, ref trimElement);
            int length = ClampViaRefEnd(ref span, start, ref trimElement);
            return span.Slice(start, length);
        }

        /// <summary>
        /// Removes all leading occurrences of a specified element.
        /// </summary>
        /// <param name="trimElement">The specified element to look for and remove.</param>
        public static ReadOnlySpan<T> TrimViaRefStart<T>(ReadOnlySpan<T> span, T trimElement)
            where T : IEquatable<T>
        {
            int start = ClampViaRefStart(ref span, ref trimElement);
            return span.Slice(start);
        }

        /// <summary>
        /// Removes all trailing occurrences of a specified element.
        /// </summary>
        /// <param name="trimElement">The specified element to look for and remove.</param>
        public static ReadOnlySpan<T> TrimViaRefEnd<T>(ReadOnlySpan<T> span, T trimElement)
            where T : IEquatable<T>
        {
            int length = ClampViaRefEnd(ref span, 0, ref trimElement);
            return span.Slice(0, length);
        }

        /// <summary>
        /// Delimits all leading occurrences of a specified element.
        /// </summary>
        /// <param name="span">The source span from which the element is removed.</param>
        /// <param name="trimElement">The specified element to look for and remove.</param>
        private static int ClampViaRefStart<T>(ref ReadOnlySpan<T> span, ref T trimElement)
            where T : IEquatable<T>
        {
            int start = 0;

            if (trimElement == null)
            {
                for (; start < span.Length; start++)
                {
                    if (span[start] != null)
                        break;
                }
            }
            else
            {
                for (; start < span.Length; start++)
                {
                    if (!trimElement.Equals(span[start]))
                        break;
                }
            }

            return start;
        }

        /// <summary>
        /// Delimits all trailing occurrences of a specified element.
        /// </summary>
        /// <param name="span">The source span from which the element is removed.</param>
        /// <param name="start">The start index from which to being searching.</param>
        /// <param name="trimElement">The specified element to look for and remove.</param>
        private static int ClampViaRefEnd<T>(ref ReadOnlySpan<T> span, int start, ref T trimElement)
            where T : IEquatable<T>
        {
            // start==len==0, start<len, start==len after ClampStart trims all
            Debug.Assert((uint)start < span.Length);

            int end = span.Length - 1;

            if (trimElement == null)
            {
                for (; end >= start; end--)
                {
                    if (span[end] != null)
                        break;
                }
            }
            else
            {
                for (; end >= start; end--)
                {
                    if (!trimElement.Equals(span[start]))
                        break;
                }
            }

            return end - start + 1;
        }

        #endregion

        #region Multiple with no guard

        /// <summary>
        /// Removes all leading and trailing occurrences of a set of elements specified
        /// in a readonly span from the span.
        /// </summary>
        /// <param name="trimElements">The span which contains the set of elements to remove.</param>
        /// <remarks>If <paramref name="trimElements"/> is empty, the span is returned unaltered.</remarks>
        public static ReadOnlySpan<T> TrimNoGuard<T>(ReadOnlySpan<T> span, ReadOnlySpan<T> trimElements)
            where T : IEquatable<T>
        {
            int start = ClampStart(span, trimElements);
            int length = ClampEnd(span, start, trimElements);
            return span.Slice(start, length);
        }

        /// <summary>
        /// Removes all leading occurrences of a set of elements specified
        /// in a readonly span from the span.
        /// </summary>
        /// <param name="trimElements">The span which contains the set of elements to remove.</param>
        /// <remarks>If <paramref name="trimElements"/> is empty, the span is returned unaltered.</remarks>
        public static ReadOnlySpan<T> TrimNoGuardStart<T>(ReadOnlySpan<T> span, ReadOnlySpan<T> trimElements)
            where T : IEquatable<T>
        {
            int start = ClampStart(span, trimElements);
            return span.Slice(start);
        }

        /// <summary>
        /// Removes all trailing occurrences of a set of elements specified
        /// in a readonly span from the span.
        /// </summary>
        /// <param name="trimElements">The span which contains the set of elements to remove.</param>
        /// <remarks>If <paramref name="trimElements"/> is empty, the span is returned unaltered.</remarks>
        public static ReadOnlySpan<T> TrimNoGuardEnd<T>(ReadOnlySpan<T> span, ReadOnlySpan<T> trimElements)
            where T : IEquatable<T>
        {
            int length = ClampEnd(span, 0, trimElements);
            return span.Slice(0, length);
        }

        #endregion

        #region Multiple via switch

        /// <summary>
        /// Removes all leading and trailing occurrences of a set of elements specified
        /// in a readonly span from the span.
        /// </summary>
        /// <param name="trimElements">The span which contains the set of elements to remove.</param>
        /// <remarks>If <paramref name="trimElements"/> is empty, the span is returned unaltered.</remarks>
        public static ReadOnlySpan<T> TrimViaSwitch<T>(ReadOnlySpan<T> span, ReadOnlySpan<T> trimElements)
            where T : IEquatable<T>
        {
            switch (trimElements.Length)
            {
                case 0:
                    return span;
                case 1:
                    return TrimViaIn(span, trimElements[0]);
            }

            int start = ClampStart(span, trimElements);
            int length = ClampEnd(span, start, trimElements);
            return span.Slice(start, length);
        }

        /// <summary>
        /// Removes all leading occurrences of a set of elements specified
        /// in a readonly span from the span.
        /// </summary>
        /// <param name="trimElements">The span which contains the set of elements to remove.</param>
        /// <remarks>If <paramref name="trimElements"/> is empty, the span is returned unaltered.</remarks>
        public static ReadOnlySpan<T> TrimViaSwitchStart<T>(ReadOnlySpan<T> span, ReadOnlySpan<T> trimElements)
            where T : IEquatable<T>
        {
            switch (trimElements.Length)
            {
                case 0:
                    return span;
                case 1:
                    return TrimViaInStart(span, trimElements[0]);
            }

            int start = ClampStart(span, trimElements);
            return span.Slice(start);
        }

        /// <summary>
        /// Removes all trailing occurrences of a set of elements specified
        /// in a readonly span from the span.
        /// </summary>
        /// <param name="trimElements">The span which contains the set of elements to remove.</param>
        /// <remarks>If <paramref name="trimElements"/> is empty, the span is returned unaltered.</remarks>
        public static ReadOnlySpan<T> TrimViaSwitchEnd<T>(ReadOnlySpan<T> span, ReadOnlySpan<T> trimElements)
            where T : IEquatable<T>
        {
            switch (trimElements.Length)
            {
                case 0:
                    return span;
                case 1:
                    return TrimViaInEnd(span, trimElements[0]);
            }

            int length = ClampEnd(span, 0, trimElements);
            return span.Slice(0, length);
        }

        #endregion

        #region Multiple via if

        /// <summary>
        /// Removes all leading and trailing occurrences of a set of elements specified
        /// in a readonly span from the span.
        /// </summary>
        /// <param name="trimElements">The span which contains the set of elements to remove.</param>
        /// <remarks>If <paramref name="trimElements"/> is empty, the span is returned unaltered.</remarks>
        public static ReadOnlySpan<T> TrimViaIf<T>(ReadOnlySpan<T> span, ReadOnlySpan<T> trimElements)
            where T : IEquatable<T>
        {
            if (trimElements.Length == 0)
                return span;
            else if (trimElements.Length == 1)
                return TrimViaIn(span, trimElements[0]);

            int start = ClampStart(span, trimElements);
            int length = ClampEnd(span, start, trimElements);
            return span.Slice(start, length);
        }

        /// <summary>
        /// Removes all leading occurrences of a set of elements specified
        /// in a readonly span from the span.
        /// </summary>
        /// <param name="trimElements">The span which contains the set of elements to remove.</param>
        /// <remarks>If <paramref name="trimElements"/> is empty, the span is returned unaltered.</remarks>
        public static ReadOnlySpan<T> TrimViaIfStart<T>(ReadOnlySpan<T> span, ReadOnlySpan<T> trimElements)
            where T : IEquatable<T>
        {
            if (trimElements.Length == 0)
                return span;
            else if (trimElements.Length == 1)
                return TrimViaIn(span, trimElements[0]);

            int start = ClampStart(span, trimElements);
            return span.Slice(start);
        }

        /// <summary>
        /// Removes all trailing occurrences of a set of elements specified
        /// in a readonly span from the span.
        /// </summary>
        /// <param name="trimElements">The span which contains the set of elements to remove.</param>
        /// <remarks>If <paramref name="trimElements"/> is empty, the span is returned unaltered.</remarks>
        public static ReadOnlySpan<T> TrimViaIfEnd<T>(ReadOnlySpan<T> span, ReadOnlySpan<T> trimElements)
            where T : IEquatable<T>
        {
            if (trimElements.Length == 0)
                return span;
            else if (trimElements.Length == 1)
                return TrimViaIn(span, trimElements[0]);

            int length = ClampEnd(span, 0, trimElements);
            return span.Slice(0, length);
        }

        #endregion

        #region Multiple via nested if

        /// <summary>
        /// Removes all leading and trailing occurrences of a set of elements specified
        /// in a readonly span from the span.
        /// </summary>
        /// <param name="trimElements">The span which contains the set of elements to remove.</param>
        /// <remarks>If <paramref name="trimElements"/> is empty, the span is returned unaltered.</remarks>
        public static ReadOnlySpan<T> TrimViaNestedIf<T>(ReadOnlySpan<T> span, ReadOnlySpan<T> trimElements)
            where T : IEquatable<T>
        {
            // Nested if optimizes for N > 1 (use other overloads for N <= 1)
            if (trimElements.Length <= 1)
            {
                return trimElements.Length == 0 ? span : TrimViaIn(span, trimElements[0]);
            }

            int start = ClampStart(span, trimElements);
            int length = ClampEnd(span, start, trimElements);
            return span.Slice(start, length);
        }

        /// <summary>
        /// Removes all leading occurrences of a set of elements specified
        /// in a readonly span from the span.
        /// </summary>
        /// <param name="trimElements">The span which contains the set of elements to remove.</param>
        /// <remarks>If <paramref name="trimElements"/> is empty, the span is returned unaltered.</remarks>
        public static ReadOnlySpan<T> TrimViaNestedIfStart<T>(ReadOnlySpan<T> span, ReadOnlySpan<T> trimElements)
            where T : IEquatable<T>
        {
            // Nested if optimizes for N > 1
            if (trimElements.Length <= 1)
            {
                return trimElements.Length == 0 ? span : TrimViaInStart(span, trimElements[0]);
            }

            int start = ClampStart(span, trimElements);
            return span.Slice(start);
        }

        /// <summary>
        /// Removes all trailing occurrences of a set of elements specified
        /// in a readonly span from the span.
        /// </summary>
        /// <param name="trimElements">The span which contains the set of elements to remove.</param>
        /// <remarks>If <paramref name="trimElements"/> is empty, the span is returned unaltered.</remarks>
        public static ReadOnlySpan<T> TrimViaNestedIfEnd<T>(ReadOnlySpan<T> span, ReadOnlySpan<T> trimElements)
            where T : IEquatable<T>
        {
            // Nested if optimizes for N > 1
            if (trimElements.Length <= 1)
            {
                return trimElements.Length == 0 ? span : TrimViaInEnd(span, trimElements[0]);
            }

            int length = ClampEnd(span, 0, trimElements);
            return span.Slice(0, length);
        }

        #endregion

        #region Marshall

        /// <summary>
        /// Removes all leading and trailing occurrences of a specified element.
        /// </summary>
        /// <param name="trimElement">The specified element to look for and remove.</param>
        public static ReadOnlyMemory<T> Trim<T>(ReadOnlyMemory<T> memory, T trimElement)
            where T : IEquatable<T>
        {
            ReadOnlySpan<T> span = memory.Span;
            int start = ClampStart(span, trimElement);
            int length = ClampEnd(span, start, trimElement);
            return memory.Slice(start, length);
        }

        /// <summary>
        /// Removes all leading occurrences of a specified element.
        /// </summary>
        /// <param name="trimElement">The specified element to look for and remove.</param>
        public static ReadOnlyMemory<T> TrimStart<T>(ReadOnlyMemory<T> memory, T trimElement)
            where T : IEquatable<T>
        {
            int start = ClampStart(memory.Span, trimElement);
            return memory.Slice(start);
        }

        /// <summary>
        /// Removes all trailing occurrences of a specified element.
        /// </summary>
        /// <param name="trimElement">The specified element to look for and remove.</param>
        public static ReadOnlyMemory<T> TrimEnd<T>(ReadOnlyMemory<T> memory, T trimElement)
            where T : IEquatable<T>
        {
            int length = ClampEnd(memory.Span, 0, trimElement);
            return memory.Slice(0, length);
        }

        /// <summary>
        /// Removes all leading and trailing occurrences of a specified element.
        /// </summary>
        /// <param name="trimElement">The specified element to look for and remove.</param>
        public static Memory<T> Trim<T>(Memory<T> memory, T trimElement)
            where T : IEquatable<T>
        {
            ReadOnlySpan<T> span = memory.Span;
            int start = ClampStart(span, trimElement);
            int length = ClampEnd(span, start, trimElement);
            return memory.Slice(start, length);
        }

        /// <summary>
        /// Removes all leading occurrences of a specified element.
        /// </summary>
        /// <param name="trimElement">The specified element to look for and remove.</param>
        public static Memory<T> TrimStart<T>(Memory<T> memory, T trimElement)
            where T : IEquatable<T>
        {
            int start = ClampStart(memory.Span, trimElement);
            return memory.Slice(start);
        }

        /// <summary>
        /// Removes all trailing occurrences of a specified element.
        /// </summary>
        /// <param name="trimElement">The specified element to look for and remove.</param>
        public static Memory<T> TrimEnd<T>(Memory<T> memory, T trimElement)
            where T : IEquatable<T>
        {
            int length = ClampEnd(memory.Span, 0, trimElement);
            return memory.Slice(0, length);
        }

        /// <summary>
        /// Removes all leading and trailing occurrences of a specified element.
        /// </summary>
        /// <param name="trimElement">The specified element to look for and remove.</param>
        public static Memory<T> TrimMarshal<T>(Memory<T> memory, T trimElement)
            where T : IEquatable<T>
            => MemoryMarshal.AsMemory(Trim((ReadOnlyMemory<T>)memory, trimElement));

        /// <summary>
        /// Removes all leading occurrences of a specified element.
        /// </summary>
        /// <param name="trimElement">The specified element to look for and remove.</param>
        public static Memory<T> TrimMarshalStart<T>(Memory<T> memory, T trimElement)
            where T : IEquatable<T>
            => MemoryMarshal.AsMemory(Trim((ReadOnlyMemory<T>)memory, trimElement));

        /// <summary>
        /// Removes all trailing occurrences of a specified element.
        /// </summary>
        /// <param name="trimElement">The specified element to look for and remove.</param>
        public static Memory<T> TrimMarshalEnd<T>(Memory<T> memory, T trimElement)
            where T : IEquatable<T>
            => MemoryMarshal.AsMemory(Trim((ReadOnlyMemory<T>)memory, trimElement));

        #endregion

        #region IndexOfAny

        /// <summary>
        /// Removes all leading and trailing occurrences of a set of elements specified
        /// in a readonly span from the span.
        /// </summary>
        /// <param name="trimElements">The span which contains the set of elements to remove.</param>
        /// <remarks>If <paramref name="trimElements"/> is empty, the span is returned unaltered.</remarks>
        public static ReadOnlySpan<T> TrimIndexOfAny<T>(ReadOnlySpan<T> span, ReadOnlySpan<T> trimElements)
            where T : IEquatable<T>
        {
            if (trimElements.Length <= 1) // Optimize for N > 1
            {
                return trimElements.Length == 0 ? span : Trim(span, trimElements[0]);
            }

            int start = ClampStartIndexOfAny(span, trimElements);
            int length = ClampEndIndexOfAny(span, start, trimElements);
            return span.Slice(start, length);
        }

        /// <summary>
        /// Delimits all leading occurrences of a specified element.
        /// </summary>
        /// <param name="span">The source span from which the element is removed.</param>
        /// <param name="trimElements">The span which contains the set of elements to remove.</param>
        private static int ClampStartIndexOfAny<T>(ReadOnlySpan<T> span, ReadOnlySpan<T> trimElements)
            where T : IEquatable<T>
        {
            int start = 0;

            for (; start < span.Length; start++)
            {
                if (span.Slice(start, 1).IndexOfAny(trimElements) < 0)
                    break;
                //if (!SequentialContains(trimElements, span[start]))
                //    break;
            }

            return start;
        }

        /// <summary>
        /// Delimits all trailing occurrences of a specified element.
        /// </summary>
        /// <param name="span">The source span from which the element is removed.</param>
        /// <param name="start">The start index from which to being searching.</param>
        /// <param name="trimElements">The span which contains the set of elements to remove.</param>
        private static int ClampEndIndexOfAny<T>(ReadOnlySpan<T> span, int start, ReadOnlySpan<T> trimElements)
            where T : IEquatable<T>
        {
            // Initially, start==len==0. If ClampStart trims all, start==len
            Debug.Assert((uint)start <= span.Length);

            int end = span.Length - 1;

            for (; end >= start; end--)
            {
                if (span.Slice(start, 1).IndexOfAny(trimElements) < 0)
                    break;
                //if (!SequentialContains(trimElements, span[end]))
                //    break;
            }

            return end - start + 1;
        }

        #endregion

        #region Contains

        /// <summary>
        /// Removes all leading and trailing occurrences of a set of elements specified
        /// in a readonly span from the span.
        /// </summary>
        /// <param name="trimElements">The span which contains the set of elements to remove.</param>
        /// <remarks>If <paramref name="trimElements"/> is empty, the span is returned unaltered.</remarks>
        public static ReadOnlySpan<T> TrimContains<T>(ReadOnlySpan<T> span, ReadOnlySpan<T> trimElements)
            where T : IEquatable<T>
        {
            if (trimElements.Length <= 1) // Optimize for N > 1
            {
                return trimElements.Length == 0 ? span : Trim(span, trimElements[0]);
            }

            int start = ClampStartContains(span, trimElements);
            int length = ClampEndContains(span, start, trimElements);
            return span.Slice(start, length);
        }

        /// <summary>
        /// Delimits all leading occurrences of a specified element.
        /// </summary>
        /// <param name="span">The source span from which the element is removed.</param>
        /// <param name="trimElements">The span which contains the set of elements to remove.</param>
        private static int ClampStartContains<T>(ReadOnlySpan<T> span, ReadOnlySpan<T> trimElements)
            where T : IEquatable<T>
        {
            int start = 0;

            for (; start < span.Length; start++)
            {
                if (!Contains(trimElements, span[start]))
                    break;
                //if (span.Slice(start, 1).IndexOfAny(trimElements) < 0)
                //    break;
                //if (!SequentialContains(trimElements, span[start]))
                //    break;
            }

            return start;
        }

        /// <summary>
        /// Delimits all trailing occurrences of a specified element.
        /// </summary>
        /// <param name="span">The source span from which the element is removed.</param>
        /// <param name="start">The start index from which to being searching.</param>
        /// <param name="trimElements">The span which contains the set of elements to remove.</param>
        private static int ClampEndContains<T>(ReadOnlySpan<T> span, int start, ReadOnlySpan<T> trimElements)
            where T : IEquatable<T>
        {
            // Initially, start==len==0. If ClampStart trims all, start==len
            Debug.Assert((uint)start <= span.Length);

            int end = span.Length - 1;

            for (; end >= start; end--)
            {
                if (!Contains(trimElements, span[start]))
                    break;
                //if (span.Slice(start, 1).IndexOfAny(trimElements) < 0)
                //    break;
                //if (!SequentialContains(trimElements, span[end]))
                //    break;
            }

            return end - start + 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Contains<T>(ReadOnlySpan<T> span, T value)
            where T : IEquatable<T>
        {
            return Contains(ref MemoryMarshal.GetReference(span), value, span.Length);
        }

        public unsafe static bool Contains<T>(ref T searchSpace, T value, int length)
               where T : IEquatable<T>
        {
            Debug.Assert(length >= 0);

            IntPtr index = (IntPtr)0; // Use IntPtr for arithmetic to avoid unnecessary 64->32->64 truncations

            if (default(T) != null || (object)value != null)
            {
                while (length >= 8)
                {
                    length -= 8;

                    if (value.Equals(Unsafe.Add(ref searchSpace, index + 0)) ||
                        value.Equals(Unsafe.Add(ref searchSpace, index + 1)) ||
                        value.Equals(Unsafe.Add(ref searchSpace, index + 2)) ||
                        value.Equals(Unsafe.Add(ref searchSpace, index + 3)) ||
                        value.Equals(Unsafe.Add(ref searchSpace, index + 4)) ||
                        value.Equals(Unsafe.Add(ref searchSpace, index + 5)) ||
                        value.Equals(Unsafe.Add(ref searchSpace, index + 6)) ||
                        value.Equals(Unsafe.Add(ref searchSpace, index + 7)))
                    {
                        goto Found;
                    }

                    index += 8;
                }

                if (length >= 4)
                {
                    length -= 4;

                    if (value.Equals(Unsafe.Add(ref searchSpace, index + 0)) ||
                        value.Equals(Unsafe.Add(ref searchSpace, index + 1)) ||
                        value.Equals(Unsafe.Add(ref searchSpace, index + 2)) ||
                        value.Equals(Unsafe.Add(ref searchSpace, index + 3)))
                    {
                        goto Found;
                    }

                    index += 4;
                }

                while (length > 0)
                {
                    length -= 1;

                    if (value.Equals(Unsafe.Add(ref searchSpace, index)))
                        goto Found;

                    index += 1;
                }
            }
            else
            {
                byte* len = (byte*)length;
                for (index = (IntPtr)0; index.ToPointer() < len; index += 1)
                {
                    if ((object)Unsafe.Add(ref searchSpace, index) is null)
                    {
                        goto Found;
                    }
                }
            }

            return false;

Found:
            return true;
        }

        #endregion
    }

    internal static class SwitchMemory
    {
        public static ReadOnlySpan<T> TrimStartIf<T>(this ReadOnlySpan<T> memory, ReadOnlySpan<T> trimElements)
            where T : IEquatable<T>
        {
            if (trimElements.Length > 1)
            {
                return memory.Slice(ClampStart(memory, trimElements));
            }

            if (trimElements.Length == 1)
            {
                return TrimStart(memory, trimElements[0]);
            }

            return memory;
        }

        public static ReadOnlySpan<T> TrimStartSwitch<T>(this ReadOnlySpan<T> memory, ReadOnlySpan<T> trimElements)
            where T : IEquatable<T>
        {
            switch (trimElements.Length)
            {
                case 0: return memory;
                case 1: return TrimStart(memory, trimElements[0]);
                default: return memory.Slice(ClampStart(memory, trimElements));
            }
        }

        private static int ClampStart<T>(ReadOnlySpan<T> span, ReadOnlySpan<T> trimElements)
            where T : IEquatable<T>
        {
            int start = 0;
            for (; start < span.Length; start++)
            {
                if (!trimElements.Contains(span[start]))
                {
                    break;
                }
            }

            return start;
        }

        public static ReadOnlySpan<T> TrimStart<T>(this ReadOnlySpan<T> memory, T trimElement)
            where T : IEquatable<T>
            => memory.Slice(ClampStart(memory, trimElement));

        private static int ClampStart<T>(ReadOnlySpan<T> span, T trimElement)
            where T : IEquatable<T>
        {
            int start = 0;

            if (trimElement != null)
            {
                for (; start < span.Length; start++)
                {
                    if (!trimElement.Equals(span[start]))
                    {
                        break;
                    }
                }
            }
            else
            {
                for (; start < span.Length; start++)
                {
                    if (span[start] != null)
                    {
                        break;
                    }
                }
            }

            return start;
        }

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Contains<T>(this ReadOnlySpan<T> span, T value)
            where T : IEquatable<T>
        {
            return Contains(ref MemoryMarshal.GetReference(span), value, span.Length);
        }

        private unsafe static bool Contains<T>(ref T searchSpace, T value, int length)
            where T : IEquatable<T>
        {
            Debug.Assert(length >= 0);

            var index = (IntPtr)0; // Use IntPtr for arithmetic to avoid unnecessary 64->32->64 truncations

            if (default(T) != null || value != null)
            {
                while (length >= 8)
                {
                    length -= 8;

                    if (value.Equals(Unsafe.Add(ref searchSpace, index + 0)) ||
                        value.Equals(Unsafe.Add(ref searchSpace, index + 1)) ||
                        value.Equals(Unsafe.Add(ref searchSpace, index + 2)) ||
                        value.Equals(Unsafe.Add(ref searchSpace, index + 3)) ||
                        value.Equals(Unsafe.Add(ref searchSpace, index + 4)) ||
                        value.Equals(Unsafe.Add(ref searchSpace, index + 5)) ||
                        value.Equals(Unsafe.Add(ref searchSpace, index + 6)) ||
                        value.Equals(Unsafe.Add(ref searchSpace, index + 7)))
                    {
                        goto Found;
                    }

                    index += 8;
                }

                if (length >= 4)
                {
                    length -= 4;

                    if (value.Equals(Unsafe.Add(ref searchSpace, index + 0)) ||
                        value.Equals(Unsafe.Add(ref searchSpace, index + 1)) ||
                        value.Equals(Unsafe.Add(ref searchSpace, index + 2)) ||
                        value.Equals(Unsafe.Add(ref searchSpace, index + 3)))
                    {
                        goto Found;
                    }

                    index += 4;
                }

                while (length > 0)
                {
                    length -= 1;

                    if (value.Equals(Unsafe.Add(ref searchSpace, index)))
                        goto Found;

                    index += 1;
                }
            }
            else
            {
                byte* len = (byte*)length;
                for (index = (IntPtr)0; index.ToPointer() < len; index += 1)
                {
                    if (Unsafe.Add(ref searchSpace, index) == null)
                    {
                        goto Found;
                    }
                }
            }

            return false;

Found:
            return true;
        }
    }
}
