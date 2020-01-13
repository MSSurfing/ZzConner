namespace Zz.Core.Data.Entity.Grpc
{
    // 原 PropertyValueType
    public enum ValueType
    {
        /// <summary>
        /// 不传值 / 不验证
        /// </summary>
        None = 0,

        /// <summary>
        /// 静态值
        /// </summary>
        StaticValue = 10,

        /// <summary>
        /// 动态范围（用于生成动态值的范围）
        /// </summary>
        DynamicRange = 20,

        /// <summary>
        /// 动态方法调用结果
        /// </summary>
        DynamicMethodProperty = 30,
    }
}
