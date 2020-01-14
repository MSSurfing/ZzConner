using System;
using System.Diagnostics.Contracts;
using System.Runtime;

namespace Zz.Core.Mock.Conner
{
    /// <summary>
    /// 实际上是 同System.Random
    /// </summary>
    internal class XRandom
    {
        private const int MBIG = Int32.MaxValue;
        private const int MSEED = 161803398;
        private const int MZ = 0;

        private int inext;
        private int inextp;
        private int[] SeedArray = new int[56];

        public XRandom() : this(Environment.TickCount) { }

        public XRandom(int Seed)
        {
            int ii;
            int mj, mk;

            //Initialize our Seed array.
            //This algorithm comes from Numerical Recipes in C (2nd Ed.)
            int subtraction = (Seed == Int32.MinValue) ? Int32.MaxValue : Math.Abs(Seed);
            mj = MSEED - subtraction;
            SeedArray[55] = mj;
            mk = 1;
            for (int i = 1; i < 55; i++)
            {  //Apparently the range [1..55] is special (Knuth) and so we're wasting the 0'th position.
                ii = (21 * i) % 55;
                SeedArray[ii] = mk;
                mk = mj - mk;
                if (mk < 0) mk += MBIG;
                mj = SeedArray[ii];
            }
            for (int k = 1; k < 5; k++)
            {
                for (int i = 1; i < 56; i++)
                {
                    SeedArray[i] -= SeedArray[1 + (i + 30) % 55];
                    if (SeedArray[i] < 0) SeedArray[i] += MBIG;
                }
            }
            inext = 0;
            inextp = 21;
            Seed = 1;
        }

        /// <summary>
        /// Return a new random number [0..1) and reSeed the Seed array.
        /// </summary>
        /// <returns></returns>
#if !FEATURE_CORECLR
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
#endif
        protected virtual double Sample()
        {
            //Including this division at the end gives us significantly improved
            //random number distribution.
            var internalSampleValue = InternalSample();
            return (internalSampleValue * (1.0 / MBIG));
        }

        private int InternalSample()
        {
            int retVal;
            int locINext = inext;
            int locINextp = inextp;

            if (++locINext >= 56) locINext = 1;
            if (++locINextp >= 56) locINextp = 1;

            retVal = SeedArray[locINext] - SeedArray[locINextp];

            if (retVal == MBIG) retVal--;
            if (retVal < 0) retVal += MBIG;

            SeedArray[locINext] = retVal;

            inext = locINext;
            inextp = locINextp;

            return retVal;
        }

        /// <returns>An int [0..Int32.MaxValue)</returns>
#if !FEATURE_CORECLR
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
#endif
        public virtual int Next()
        {
            return InternalSample();
        }

        public double GetSampleForLargeRange()
        {
            // The distribution of double value returned by Sample 
            // is not distributed well enough for a large range.
            // If we use Sample for a range [Int32.MinValue..Int32.MaxValue)
            // We will end up getting even numbers only.

            int result = InternalSample();
            // Note we can't use addition here. The distribution will be bad if we do that.
            bool negative = (InternalSample() % 2 == 0) ? true : false;  // decide the sign based on second sample
            if (negative)
            {
                result = -result;
            }
            double d = result;
            d += (Int32.MaxValue - 1); // get a number in range [0 .. 2 * Int32MaxValue - 1)
            d /= 2 * (uint)Int32.MaxValue - 1;
            return d;
        }

        /// <param name="minValue">the least legal value for the Random number.</param>
        /// <param name="maxValue">One greater than the greatest legal return value.</param>
        /// <returns>An int [0..Int32.MaxValue)</returns>
        public virtual int Next(int minValue, int maxValue)
        {
            if (minValue > maxValue)
            {
                throw new ArgumentOutOfRangeException("minValue", "Argument_MinMaxValue minValue maxValue");
            }
            Contract.EndContractBlock();

            long range = (long)maxValue - minValue;
            if (range <= (long)Int32.MaxValue)
            {
                var sampleValue = Sample();
                return ((int)(sampleValue * range) + minValue);
            }
            else
            {
                return (int)((long)(GetSampleForLargeRange() * range) + minValue);
            }
        }


        /*=====================================Next=====================================
        **Returns: An int [0..maxValue)
        **Arguments: maxValue -- One more than the greatest legal return value.
        ==============================================================================*/
        public virtual int Next(int maxValue)
        {
            if (maxValue < 0)
            {
                throw new ArgumentOutOfRangeException("maxValue", "ArgumentOutOfRange_MustBePositive maxValue");
            }
            Contract.EndContractBlock();
            return (int)(Sample() * maxValue);
        }


        /*=====================================Next=====================================
        **返回的是一个区间: A double [0..1) ,该区间 大于等于0, 小于1。
        ==============================================================================*/
        /// <summary>
        /// 
        /// </summary>
        /// <returns>A double [0..1)</returns>
#if !FEATURE_CORECLR
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
#endif
        public virtual double NextDouble()
        {
            return Sample();
        }

        /// <summary>
        /// 用随机字节[0..0x7f]填充字节数组
        /// </summary>
        /// <param name="buffer">被填充的数组.</param>
        public virtual void NextBytes(byte[] buffer)
        {
            if (buffer == null) throw new ArgumentNullException("buffer");
            Contract.EndContractBlock();
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = (byte)(InternalSample() % (Byte.MaxValue + 1));
            }
        }
    }
}
