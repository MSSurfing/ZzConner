using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zz.Core.Data.Entity.Grpc;
using Zz.Http.Core.Controllers;
using Zz.Services.Grpc;

namespace Zz.Api.Controllers
{
    [Route("api/[controller]")]
    public class ServiceInfoController : BaseController
    {
        #region Fields
        private readonly IServiceInfoService _serviceInfoService;
        #endregion

        #region Ctor
        public ServiceInfoController(IServiceInfoService serviceInfoService)
        {
            _serviceInfoService = serviceInfoService;
        }
        #endregion

        [Route("Index"), HttpGet]
        public IActionResult Index()
        {
            return Ok();
        }

        [HttpGet, Route("add")]
        public IActionResult Add()
        {
            var serviceInfo = new ServiceInfo()
            {
                Id = Guid.NewGuid(),
                Name = "Ab",
                AssemblyQualifiedName = "abc",
                Deleted = false,
            };

            serviceInfo.Methods.Add(new Method()
            {
                Name = "fdsf",
                RequestModel = new RequestModel
                {
                    Name = "fdfdf",
                    AssemblyQualifiedName = "fdsfdf",
                },
                ResponseModel = new ResponseModel
                {
                    Name = "fsdfds",
                    AssemblyQualifiedName = "fdf",
                }
            });

            _serviceInfoService.Insert(serviceInfo);
            //DebugLogger.Debug($"inserted user:id{user.Id}");
            return OkMsg(true);
        }
    }
}