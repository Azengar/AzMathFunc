using System;
using System.Collections.Generic;
using System.Text;

namespace AzMathFunc
{
    /// <summary>
    /// A utility class containing functions to easily created <see cref="SampledFunc"/> of dice probability functions
    /// </summary>
    public static class DiceProbFuncs
    {
        /// <summary>
        /// Get the sampled function of a dice probability function
        /// </summary>
        /// <param name="format">The format of the dice roll, formatted "xdy" where x is the amount of dices thrown and y the amount of faces on a dice</param>
        /// <returns>The sampled function of the dice throw</returns>
        public static SampledFunc GetDiceProbFunc(string format)
        {
            string[] split = format.Trim().Split('d');
            return GetDiceProbFunc(int.Parse(split[0]), int.Parse(split[1]));
        }

        /// <summary>
        /// Get the sampled function of a dice probability function
        /// </summary>
        /// <param name="count">The amount of dices thrown</param>
        /// <param name="dice">The amount of faces on a dice</param>
        /// <returns>The sampled function of the dice throw</returns>
        public static SampledFunc GetDiceProbFunc(int count, int dice)
        {
            float prob = (1f / MathF.Pow(dice, count));
            SortedList<int, float> probs = new SortedList<int, float>();

            for (int i = 0; i < dice; i++)
            {
                probs.Add(i + count, prob * (i + 1));
            }

            for (int i = 0; i < dice - 1; i++)
            {
                probs.Add(i + dice + count, prob * (dice - (i + 1)));
            }

            return new SampledFunc(probs);
        }
    }
}
