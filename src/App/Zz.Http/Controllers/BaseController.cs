using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Zz.Core;
using Zz.Http.Core.Mvc.Results;

namespace Zz.Http.Core.Controllers
{
    [ApiController]
    [Diagnostics.Stopwatch]
    public abstract class BaseController : ControllerBase
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

        protected virtual IActionResult BadMsg(object message = null)
        {
            return Ok(new MessageResult { Code = MessageCode.Error, Message = message });
        }

        protected virtual IActionResult OkMsg(object Data = null)
        {
            return Ok(new MessageResult { Code = MessageCode.Success, Data = Data });
        }
        #endregion

        #region Extenions for paged list
        protected virtual IActionResult OkPaged(int total, object rows)
        {
            return OkMsg(new { total, rows });
        }

        protected virtual IActionResult OkPaged<T>(IPagedList<T> pagedList)
        {
            return OkPaged(pagedList.TotalCount, pagedList);
        }
        #endregion
    }


}
