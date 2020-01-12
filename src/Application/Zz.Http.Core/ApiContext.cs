using System;
using System.Collections.Generic;
using System.Text;
using Zz.Core;
using Zz.Core.Data.Entity.Users;
using Zz.Services.Authentication;

namespace Zz.Http.Core
{
    public partial class ApiContext : IApiContext
    {
        #region Fields
        private readonly IZzAuthenticationService _zzAuthenticationService;

        private User _cachedUser;
        #endregion

        #region Ctor
        public ApiContext(IZzAuthenticationService authenticationService)
        {
            _zzAuthenticationService = authenticationService;
        }
        #endregion

        public User CurrentUser
        {
            get
            {
                if (_cachedUser != null)
                    return _cachedUser;

                User customer = null;
                var exUser = _zzAuthenticationService.GetAuthenticatedUser();
                if (exUser != null)
                    customer = exUser.User;

                if (customer != null && !customer.Deleted && customer.Active)
                {
                    _cachedUser = customer;
                }

                return _cachedUser;
            }
            set { _cachedUser = value; }
        }
    }
}
