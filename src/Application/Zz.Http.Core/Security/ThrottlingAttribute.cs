using Autofac.Engine;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using Zz.Core.Caching;

namespace Zz.Http.Core.Security
{
    public class ThrottlingAttribute : TypeFilterAttribute
    {
        #region Fields & Consts
        //ToImprove, （注意：有延续缓存，导致长时间缓存有效）
        private const double THROTTLING_TIME = 0.016;               //约1秒
        private readonly static string THROTTLING_KEY = "sf.api.throttling.ip-{0}-{1}";
        #endregion

        public ThrottlingAttribute() : base(typeof(ThrottlingFilter)) { }

        #region Nested Filter
        private class ThrottlingFilter : IActionFilter
        {


            #region Utilities
            private bool Executing(string ip, int maxTimes = 0)
            {
                // Todo
                //var cacheManager = EngineContext.Resolve<ICacheManager>();  //IStaticCacheManager
                //string patternKey = string.Format(THROTTLING_KEY, ip, "");
                //if (cacheManager.Get<int>(patternKey) > maxTimes)
                //    return false;

                //string key = string.Format(THROTTLING_KEY, ip, System.DateTime.Now.Millisecond);
                //cacheManager.SetInce...(key, 1, THROTTLING_TIME);
                return true;
            }
            #endregion

            public void OnActionExecuting(ActionExecutingContext context)
            {
                if (context == null || context.HttpContext.Request == null)
                    return;

                //Todo, Settings
                //only GET requests
                if (!string.Equals(context.HttpContext.Request.Method, "GET", StringComparison.OrdinalIgnoreCase))
                    return;

                // Todo

            }
            public void OnActionExecuted(ActionExecutedContext context) { }
        }
        #endregion
    }
}
