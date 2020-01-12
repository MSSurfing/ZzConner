using System;
using System.Security.Cryptography;
using System.Text;
using Zz.Core.Caching;

namespace Zz.Services.Authentication.OAuth
{
    /// <summary>
    /// 应用授权器
    /// </summary>
    public class ApplicationOAuthProvider : IOAuthProvider
    {
        #region consts 
        private const int SALT_SIZE = 36;
        #endregion

        #region Fields
        private readonly ICacheManager _cacheManager;
        #endregion

        #region Ctor
        public ApplicationOAuthProvider(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }
        #endregion

        #region Utilities
        protected virtual string CreateSalt(int size)
        {
            //生成随机密钥
            var rng = new RNGCryptoServiceProvider();
            var buff = new byte[size];
            rng.GetBytes(buff);

            return Convert.ToBase64String(buff);
        }

        /// <summary>
        /// 生成Hash密文
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="saltkey"></param>
        /// <returns></returns>
        public virtual string CreateToken(string identity, string saltkey)
        {
            var tBytes = Encoding.UTF8.GetBytes(String.Concat(identity, saltkey));

            var hBytes = HashAlgorithm.Create("SHA1").ComputeHash(tBytes);
            return BitConverter.ToString(hBytes).Replace("-", "");
        }
        #endregion

        /// <summary>
        /// 刷新Token
        /// </summary>
        /// <param name="expireTime">有效防问时间（分钟）</param>
        /// <param name="identity">用户标识</param>
        /// <returns>token</returns>
        public string RefreshToken(int expireTime, string identity)
        {
            var salt = CreateSalt(SALT_SIZE);
            var token = CreateToken(identity, salt);

            if (_cacheManager.IsSet(token))
                _cacheManager.Remove(token);

            _cacheManager.Set(token, identity, expireTime);

            return token;
        }

        public void ReplaceToken(int expireTime, string token, string identity)
        {
            if (_cacheManager.IsSet(token))
                _cacheManager.Remove(token);

            _cacheManager.Set(token, identity, expireTime);
        }

        /// <summary>
        /// 返回用户标识
        /// </summary>
        /// <param name="token">token</param>
        /// <returns>用户标识</returns>
        public string GetIdentity(string token)
        {
            if (!_cacheManager.IsSet(token))
                return null;

            return _cacheManager.Get<string>(token);
        }

        public void RemoveIdentity(string token)
        {
            if (string.IsNullOrEmpty(token))
                return;

            if (!_cacheManager.IsSet(token))
                _cacheManager.Remove(token);
        }

        /// <summary>
        /// 验证token
        /// </summary>
        /// <param name="token">token</param>
        /// <returns>验证有效性</returns>
        public bool ValidateTokenRequest(string token)
        {
            return _cacheManager.IsSet(token);
        }
    }
}
