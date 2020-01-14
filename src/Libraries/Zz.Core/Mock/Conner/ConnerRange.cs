using System;

namespace Zz.Core.Mock.Conner
{
    internal struct MockRange
    {
        public const int Divisor = 100;
    }

    public struct MInt64
    {
        public const Int64 MinValue = Int64.MinValue / MockRange.Divisor;
        public const Int64 MaxValue = Int64.MaxValue / MockRange.Divisor;
    }

    public struct MUInt64
    {
        public const UInt64 MinValue = UInt64.MinValue;
        public const UInt64 MaxValue = UInt64.MaxValue / MockRange.Divisor;
    }

    public struct MFloat
    {
        public const Single MinValue = Single.MinValue / MockRange.Divisor;
        public const Single MaxValue = Single.MaxValue / MockRange.Divisor;
    }

    public struct MDouble
    {
        public const double MinValue = Double.MinValue / MockRange.Divisor;
        public const double MaxValue = Double.MaxValue / MockRange.Divisor;
    }

    public struct MDecimal
    {
        public const decimal MinValue = decimal.MinValue / MockRange.Divisor;
        public const decimal MaxValue = decimal.MaxValue / MockRange.Divisor;
    }
}
