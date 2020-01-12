using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Zz.Http.Core.Controllers;

namespace Zz.Http.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : BaseAuthorizeController
    {
        [Route("Index"), HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}