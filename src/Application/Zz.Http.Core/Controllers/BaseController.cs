using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Zz.Http.Core.Mvc.Results;

namespace Zz.Http.Core.Controllers
{
    [Diagnostics.Stopwatch]
    public abstract class BaseController : Controller
    {
        #region Json Result
        /*
         * Todo,
         *  使用 await Task.Run()...
         *  MessageResult 继承：ActionResult
         * 
         */

        protected virtual IActionResult EmptyMessage()
        {
            return new EmptyResult();
        }

        protected virtual IActionResult EtagMessage(object Data = null, string Key = null)
        {
            //var etagService = EngineContext.Resolve<IETagService>();
            //return Request.SuccessMessage(Data: new { data = Data, etag = etagService.GetTag(Key) });
            return new EmptyResult();
        }

        protected virtual IActionResult ErrorMessage(object message = null)
        {
            return Json(new MessageResult { Code = MessageCode.Error, Message = message });
        }

        protected virtual IActionResult SuccessMessage(object Data = null)
        {
            return Json(new MessageResult { Code = MessageCode.Success, Data = Data });
        }
        #endregion
    }
}
