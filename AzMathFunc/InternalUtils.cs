using System;
using System.Collections.Generic;
using System.Text;

namespace AzMathFunc
{
    /// <summary>
    /// A collection of internal utility functions
    /// </summary>
    internal static class InternalUtils
    {
        /// <summary>
        /// Linearly interpolate between two values with a rate
        /// </summary>
        /// <param name="a">The lower value</param>
        /// <param name="b">The upper value</param>
        /// <param name="t">The rate</param>
        /// <returns>The linearly interpolated value</returns>
        public static float Lerp(float a, float b, float t) => a * (1f - t) + b * t;

        /// <summary>
        /// Gets the min and max values out of an enumerable
        /// </summary>
        /// <typeparam name="T">The type of the values</typeparam>
        /// <param name="values">The values</param>
        /// <param name="minValue">The min value of the type T</param>
        /// <param name="maxValue">The max value of the type T</param>
        /// <param name="min">The min value of the values</param>
        /// <param name="max">The max value of the values</param>
        public static void GetMinMax<T>(IEnumerable<T> values, T minValue, T maxValue, out T min, out T max) where T : IComparable<T>
        {
            min = maxValue;
            max = minValue;
            foreach (T t in values)
            {
                if (t.CompareTo(min) < 0) min = t;
                if (t.CompareTo(max) > 0) max = t;
            }
        }
    }
}
