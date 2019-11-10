using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzMathFunc
{
    /// <summary>
    /// Represents a mathematical function sampled with a list of samples, values outside of samples will be linearly interpolated
    /// </summary>
    public class SampledFunc
    {
        private const string ConsoleBarChar = "*";

        /// <summary>
        /// The count of samples used in this function
        /// </summary>
        public int SamplesCount => m_Samples.Count;

        private SortedList<int, float> m_Samples = new SortedList<int, float>();

        private readonly int m_MinKey;
        private readonly int m_MaxKey;

        private readonly float m_MinValue;
        private readonly float m_MaxValue;

        /// <summary>
        /// Create a new sampled function
        /// </summary>
        /// <param name="samples">The samples that represent the function</param>
        public SampledFunc(SortedList<int, float> samples)
        {
            m_Samples = samples;
            InternalUtils.GetMinMax(m_Samples.Keys, int.MinValue, int.MaxValue, out m_MinKey, out m_MaxKey);
            InternalUtils.GetMinMax(m_Samples.Values, float.MinValue, float.MaxValue, out m_MinValue, out m_MaxValue);
        }

        /// <summary>
        /// Sample the function with an x value to get an y value, if the x value is between two existing samples its y will be interpolated
        /// </summary>
        /// <param name="x">The x value</param>
        /// <returns>The interpolated y value</returns>
        public float Sample(float x)
        {
            int a = (int)MathF.Floor(x);
            int b = (int)MathF.Ceiling(x);
            if (a == b) b++;

            if (b > SamplesCount) return m_Samples[a];
            return InternalUtils.Lerp(m_Samples[a], m_Samples[b], x - a);
        }

        /// <summary>
        /// Gets the cumulative version of this function where samples are summed
        /// </summary>
        /// <returns>The cumulative sampled function</returns>
        public SampledFunc GetCumulative()
        {
            SortedList<int, float> cumulativeProbs = new SortedList<int, float>();

            float cumul = 0f;
            foreach (var pair in m_Samples)
            {
                cumul += pair.Value;
                cumulativeProbs.Add(pair.Key, cumul);
            }

            return new SampledFunc(cumulativeProbs);
        }

        /// <summary>
        /// Gets the inverse of this function (f-1) where f(y) = x
        /// </summary>
        /// <returns>The inversion function</returns>
        public SampledFunc GetInverse()
        {
            SortedList<int, float> inverseProbs = new SortedList<int, float>();

            foreach (var pair in m_Samples)
            {
                inverseProbs.Add(pair.Key, (float)pair.Key / m_MaxKey);
            }

            return new SampledFunc(inverseProbs);
        }

        /// <summary>
        /// Plot the values of this function to the console, if the function is supersampled, the additional values will be interpolated
        /// </summary>
        /// <param name="supersampling">Multiply the amount of samples by this value</param>
        public void PlotToConsole(float supersampling = 1f)
        {
            StringBuilder builder = new StringBuilder();
            float scale = 70f / m_MaxValue;
            float step = SamplesCount / (supersampling * SamplesCount);

            float current = m_MinKey;
            while (current <= SamplesCount + 1)
            {
                float value = Sample(current);
                int round = (int)(value * scale);
                for (int i = 0; i < round; i++) builder.Append(ConsoleBarChar);

                Console.WriteLine("{0:0.00}\t\t{1:0.00}%\t\t{2}", current, value * 100f, builder.ToString());
                builder.Clear();

                current += step;
            }
        }
    }
}
