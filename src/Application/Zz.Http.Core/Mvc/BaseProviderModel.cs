namespace Zz.Http.Core.Mvc
{
    /// <summary>
    /// 供商模型
    /// </summary>
    public partial class BaseProviderModel : BaseModel
    {
        /// <summary>
        /// 供商系统名称
        /// </summary>
        public virtual string ProviderSystemName { get; set; }
        /// <summary>
        /// 访问Token
        /// </summary>
        public virtual string OAuthToken { get; set; }
    }
}
