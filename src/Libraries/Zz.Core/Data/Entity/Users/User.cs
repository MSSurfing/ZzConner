using System;

namespace Zz.Core.Data.Entity.Users
{
    public partial class User : BaseEntity
    {
        #region Properties
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobilephone { get; set; }
        public string Password { get; set; }

        /// <summary>
        /// 启用
        /// </summary>
        public bool Active { get; set; }
        /// <summary>
        /// 删除
        /// </summary>
        public bool Deleted { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedOn { get; set; }
        #endregion
    }
}
