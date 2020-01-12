using Autofac.Engine;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using Zz.Services.Authentication;

namespace Zz.Http.Core.Security
{
    public class HttpAuthorizedAttribute : TypeFilterAttribute
    {
        #region Fields
        private readonly bool _allowAnonmyous;
        #endregion
        public HttpAuthorizedAttribute(bool allowAnonmyous = false)
            : base(typeof(HttpAuthorizedFilter))
        {
            _allowAnonmyous = allowAnonmyous;
            base.Arguments = new object[] { allowAnonmyous };
        }

        public bool AllowAnonmyous => _allowAnonmyous;

        #region Nested Filter
        private class HttpAuthorizedFilter : IActionFilter
        {
            #region Fields
            private readonly bool _allowAnonmyous;
            #endregion

            public HttpAuthorizedFilter(bool allowAnonmyous)
            {
                this._allowAnonmyous = allowAnonmyous;
            }

            public void OnActionExecuted(ActionExecutedContext context) { }

            public void OnActionExecuting(ActionExecutingContext context)
            {

                if (this._allowAnonmyous)
                    return;

#if Develop
                if (this._allowAnonmyous || !this._allowAnonmyous)
                    return;
#endif

                if (context == null || context.Result == null)
                    return;

                var authentication = EngineContext.Resolve<IZzAuthenticationService>();
            }
        }
        #endregion
    }
}
