using System;
using Zz.Core.Data.Entity.Metadata;

namespace Zz.Core.Data.Entity.Grpc
{
    // Zz DG 中的 PropertyModel
    public class Property : ITypeModel
    {
        #region Properties
        public Guid MethodId { get; set; }
        public Guid? RequestModelId { get; set; }
        public Guid? ResponseModelId { get; set; }

        public int DataTypeId { get; set; }
        public int ValueTypeId { get; set; }

        public Guid? PropertyValueId { get; set; }
        #endregion

        #region Value range Properties

        /// <summary>
        /// 静态值
        /// </summary>
        public string StaticValue { get; set; }

        public string MinValue { get; set; }
        public string MaxValue { get; set; }

        #endregion

        #region Navigation Properties
        // 直接挂上 消息对象所在的 Method
        public virtual Method MethodInfo { get; set; }
        public virtual RequestModel RequestModel { get; set; }
        public virtual ResponseModel ResponseModel { get; set; }

        public virtual DataType DataType { get => (DataType)this.DataTypeId; set => this.DataTypeId = (int)value; }

        /// <summary>
        /// 值类型
        /// </summary>
        public virtual ValueType ValueType { get => (ValueType)this.ValueTypeId; set => this.ValueTypeId = (int)value; }

        // 可以 为空
        public virtual Property PropertyValue { get; set; }
        #endregion
    }
}
