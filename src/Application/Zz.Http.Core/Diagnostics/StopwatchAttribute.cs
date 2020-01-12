using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Zz.Http.Core.Diagnostics
{
    public class StopwatchAttribute : TypeFilterAttribute
    {
        #region Fields Consts
        public const string STOP_WATCH_KEY = "zz.debug.stopwatch";
        #endregion

        #region Ctor
        public StopwatchAttribute() : base(typeof(StopwatchFilter))
        {
            // Todo, Settings;
        }
        #endregion

        #region Nested Filter
        private class StopwatchFilter : IActionFilter
        {
            public StopwatchFilter()
            {
                //Todo, Settings;
            }

            public void OnActionExecuted(ActionExecutedContext context)
            {
                if (context == null || context.HttpContext.Request == null)
                    return;

                var watch = context.HttpContext.Items[STOP_WATCH_KEY] as Stopwatch;
                if (watch == null)
                    return;

                watch.Stop();
                //Todo logo
            }

            public void OnActionExecuting(ActionExecutingContext context)
            {
                if (context == null || context.HttpContext.Request == null)
                    return;

                var watch = new Stopwatch();
                context.HttpContext.Items[STOP_WATCH_KEY] = watch;
                watch.Start();
            }
        }
        #endregion
    }
}
