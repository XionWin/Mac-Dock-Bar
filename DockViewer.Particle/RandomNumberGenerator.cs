using System;

namespace Effect.Lib
{
    public class RandomNumberGenerator
    {
        private Random mRandom; // store the random object

        #region Constructor

        /// <summary>
        /// Creates a random number generator given a seed
        /// </summary>
        /// <param name="seed"></param>
        public RandomNumberGenerator(int seed)
        {
            mRandom = new Random(seed);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns the next double no greater than max
        /// </summary>
        /// <param name="max"></param>
        /// <returns></returns>
        public double NextDouble(double max)
        {
            return mRandom.NextDouble() * max;
        }

        /// <summary>
        /// Returns the next double between min and max
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public double NextDouble(double min, double max)
        {
            if (min > max)
                return mRandom.NextDouble() * (min - max) + max;
            else
                return mRandom.NextDouble() * (max - min) + min;
        }

        #endregion
    }
}
