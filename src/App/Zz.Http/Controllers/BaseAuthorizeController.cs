using System;
using System.Collections.Generic;
using System.Text;
using Zz.Http.Core.Security;

namespace Zz.Http.Core.Controllers
{
    [Throttling]
    [HttpAuthorized]
    public abstract class BaseAuthorizeController : BaseController
    {

    }
}
