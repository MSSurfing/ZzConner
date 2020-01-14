using System;
using Zz.Core.Mock.Conner;

namespace Zz.Core.Mock
{
    public interface IMocker
    {
        int Next(int minValue = Int32.MinValue, int maxValue = Int32.MaxValue);

        long NextInt64(long minValue = MInt64.MinValue, long maxValue = MInt64.MaxValue);

        uint NextUInt32(uint minValue = UInt32.MinValue, uint maxValue = UInt32.MaxValue);

        ulong NextUInt64(ulong minValue = MUInt64.MinValue, ulong maxValue = MUInt64.MaxValue);

        double NextDouble(double minValue = MDouble.MinValue, double maxValue = MDouble.MaxValue);

        float NextFloat(float minValue = MFloat.MinValue, float maxValue = MFloat.MaxValue);

        decimal NextDecimal(decimal minValue = MDecimal.MinValue, decimal maxValue = MDecimal.MaxValue);

        bool NextBool();

        byte[] NextBytes(int length);

        string NextString(int length);

        DateTime NextDateTime(long minTicks = 0, long maxTicks = 3155378975999999999 + 1);

        #region Extensions
        // ToImprove zh_cn、ja_jp
        string NextText(int length);

        string NextChineseName();

        TEnum Next<TEnum>(TEnum[] ranges) where TEnum : struct;

        TEnum NextEnum<TEnum>() where TEnum : struct;

        int NextEnum(Type enumType);
        #endregion
    }
}
