namespace Zz.Services.Authentication.OAuth
{
    /// <summary>
    /// 授权器
    /// </summary>
    public interface IOAuthProvider
    {
        /// <summary>
        /// 刷新Token
        /// </summary>
        /// <param name="expireTime">有效防问时间（分钟）</param>
        /// <param name="identity">用户标识</param>
        /// <returns>token</returns>
        string RefreshToken(int expireTime, string identity);

        /// <summary>
        /// 替换原Token
        /// </summary>
        /// <param name="expireTime"></param>
        /// <param name="token"></param>
        void ReplaceToken(int expireTime, string token, string identity);

        /// <summary>
        /// 返回用户标识
        /// </summary>
        /// <param name="token">token</param>
        /// <returns>用户标识</returns>
        string GetIdentity(string token);

        /// <summary>
        /// 移除Token
        /// </summary>
        /// <param name="token"></param>
        void RemoveIdentity(string token);

        /// <summary>
        /// 验证token
        /// </summary>
        /// <param name="token">token</param>
        /// <returns>验证有效性</returns>
        bool ValidateTokenRequest(string token);
    }
}
