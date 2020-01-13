using Zz.Core.Data.Entity.Metadata;

namespace Zz.Core.Data.Entity.Grpc
{
    // Zz DG 中的 PropertyModel
    public class Property : ITypeModel
    {
        // 直接挂上 消息对象所在的 Method
        public virtual Method MethodInfo { get; set; }
        public virtual RequestModel MessageInfo { get; set; }

        public virtual DataType DataType { get; set; }

        /// <summary>
        /// 值类型
        /// </summary>
        public virtual ValueType ValueType { get; set; }

        #region Value range
        /// <summary>
        /// 静态值
        /// </summary>
        public virtual string StaticValue { get; set; }

        public virtual string MinValue { get; set; }
        public virtual string MaxValue { get; set; }

        // 可以 为空
        public virtual Property PropertyValue { get; set; }

        //public virtual Guid? PropertyValueId { get; set; }
        #endregion
    }
}
