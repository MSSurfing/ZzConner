using System;
using Microsoft.AspNetCore.Mvc;
using Zz.Core.Data.Entity.Users;
using Zz.Http.Api.Models.Users;
using Zz.Http.Core.Controllers;
using Zz.Services.Authentication;
using Zz.Services.Users;

namespace Zz.Http.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Token")]
    public class TokenController : BaseAuthorizeController
    {
        private readonly IUserService _userService;
        private readonly IZzAuthenticationService _zzAuthenticationService;

        public TokenController(IUserService userService, IZzAuthenticationService zzAuthenticationService)
        {
            _userService = userService;
            _zzAuthenticationService = zzAuthenticationService;
        }

        [Route("Index")]
        [HttpGet]
        public IActionResult Token(LoginModel model)
        {
            if (model == null)
                return BadMsg("无效");

            var user = _userService.GetByMobilephone(model.Mobilephone);
            if (user.Deleted)
                return BadMsg("无效");

            var token = _zzAuthenticationService.RefreshToken(30, new ExternalUser() { Id = Guid.NewGuid() });

            return OkMsg(new { token });
        }
    }
}