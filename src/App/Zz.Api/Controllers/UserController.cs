using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Zz.Core.Data.Entity.Users;
using Zz.Http.Core.Controllers;
using Zz.Services.Users;

namespace Zz.Http.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : BaseAuthorizeController
    {
        #region Fields
        private readonly IUserService _userService;
        #endregion

        #region Ctor
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        #endregion

        [Route("Index"), HttpGet]
        public IActionResult Index()
        {
            return Ok();
        }

        [HttpGet, Route("adduser")]
        public IActionResult Add()
        {
            var user = new User()
            {
                Id = Guid.NewGuid(),
                Name = "Ab",
                Email = "Ab@ab.ab",
                Mobilephone = "18000009999",
                Active = true,
                Deleted = false,
            };
            _userService.Insert(user);
            //DebugLogger.Debug($"inserted user:id{user.Id}");
            return OkMsg(true);
        }
    }
}