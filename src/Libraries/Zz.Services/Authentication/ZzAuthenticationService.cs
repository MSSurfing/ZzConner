using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Zz.Core.Data.Entity.Users;
using Zz.Services.Authentication.OAuth;
using Zz.Services.Users.External;

namespace Zz.Services.Authentication
{
    public partial class ZzAuthenticationService : IZzAuthenticationService
    {
        #region Fields & Consts
        // must be lower
        private const string HEADER_TOKEN_KEY = "zz_authentication";
        private const int TOKEN_LENTH = 36;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOAuthProvider _oAuthProvider;
        private readonly IExternalUserService _exUserService;

        private ExternalUser _cachedUser;
        #endregion

        #region Ctor
        public ZzAuthenticationService(IOAuthProvider oAuthProvider
            , IExternalUserService exUserService, IHttpContextAccessor httpContextAccessor)
        {
            _oAuthProvider = oAuthProvider;
            _exUserService = exUserService;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion

        #region Utilities
        protected virtual ExternalUser GetAuthenticatedUserByTicket(string ticket)
        {
            if (string.IsNullOrEmpty(ticket))
                return null;

            Guid guid;
            if (Guid.TryParse(ticket, out guid))
                return _exUserService.GetById(guid);

            return null;
        }

        protected virtual string GetRequestTicketToken()
        {
            if (_httpContextAccessor == null || _httpContextAccessor.HttpContext.Request == null)
                return null;

            //不能使用 AllKeys.Contains，在安卓环境下所有的header均会被换成小写，在其他环境会以大小写提交，而AllKeys.Contains分区大小写匹配，会造成有大写的key在安桌环境下提交时检测不到。
            //!_httpContext.Request.Headers.AllKeys.Contains(HEADER_TOKEN_KEY)

            var sfToken = _httpContextAccessor.HttpContext.Request.Headers[HEADER_TOKEN_KEY];         //这里会忽略大小写
            return sfToken;
        }
        #endregion

        #region Methods

        public bool Authorized()
        {
            if (_cachedUser != null)
                return true;

            var ticketToken = GetRequestTicketToken();
            if (ticketToken == null || ticketToken.Length < TOKEN_LENTH)
                return false;

            var token = ticketToken.Substring(TOKEN_LENTH);
            if (_oAuthProvider.ValidateTokenRequest(token))
                return true;

            var ticket = ticketToken.Substring(0, TOKEN_LENTH);
            var exUser = GetAuthenticatedUserByTicket(ticket);
            if (exUser == null || !exUser.AccessToken.Equals(token, StringComparison.OrdinalIgnoreCase))
                return false;

            _oAuthProvider.ReplaceToken(30, exUser.AccessToken, exUser.Id.ToString());
            _cachedUser = exUser;
            return true;
        }

        public string RefreshToken(int expireTime, ExternalUser exUser)
        {
            if (exUser == null)
                throw new ArgumentNullException(nameof(exUser));

            var oauthToken = _oAuthProvider.RefreshToken(expireTime: expireTime, identity: exUser.Id.ToString());

            exUser = _exUserService.GetById(exUser.Id);
            exUser.AccessToken = oauthToken;
            _exUserService.Update(exUser);

            return exUser.Id.ToString() + oauthToken;
        }

        public ExternalUser GetAuthenticatedUser()
        {
            if (_cachedUser != null)
                return _cachedUser;

            var ticketToken = GetRequestTicketToken();
            if (ticketToken == null || ticketToken.Length < TOKEN_LENTH)
                return null;

            var token = ticketToken.Substring(TOKEN_LENTH);
            var identity = _oAuthProvider.GetIdentity(token);
            return GetAuthenticatedUserByTicket(identity);
        }

        public void SignOut()
        {
            var ticketToken = GetRequestTicketToken();
            SignOut(ticketToken);
        }

        public void SignOut(string token)
        {
            if (token == null || token.Length < TOKEN_LENTH)
                return;

            _oAuthProvider.RemoveIdentity(token.Substring(TOKEN_LENTH));
        }
        #endregion
    }
}
