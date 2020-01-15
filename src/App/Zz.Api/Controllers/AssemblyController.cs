using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zz.Api.Models.Assemblies;
using Zz.Core.Data.Entity.Metadata;
using Zz.Http.Core.Controllers;
using Zz.Services.Media;
using Zz.Services.Metadata;

namespace Zz.Api.Controllers
{
    [Route("api/[controller]")]
    public class AssemblyController : BaseController
    {
        #region Fields
        private readonly IAssemblyService _assemblyService;
        private readonly IFileInfoService _fileInfoService;
        #endregion

        #region Ctor
        public AssemblyController(IAssemblyService assemblyService, IFileInfoService fileInfoService)
        {
            _assemblyService = assemblyService;
            _fileInfoService = fileInfoService;
        }
        #endregion

        #region Preparation
        protected virtual AssemblyModel ToModel(Assembly entity)
        {
            return new AssemblyModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Filename = entity.Filename,
                // Todo
            };
        }
        #endregion

        [Route("List"), HttpGet]
        public IActionResult List() // Request cmd
        {
            var list = _assemblyService.Search(0, 10);

            return OkPaged(list.TotalCount, list.Select(ToModel));
        }

        [Route("Edit"), HttpPost]
        public IActionResult Edit(Assembly model)
        {
            return OkMsg();
        }

        [Route("Upload"), HttpPost]
        public IActionResult Upload(IFormFile assemblyFile)
        {
            if (assemblyFile == null || assemblyFile.Length == 0)
            {
                return BadMsg("UploadFile.Error");
            }

            // Todo, must be *.dll
            var fileInfo = _fileInfoService.SaveInFile(assemblyFile);

            var entity = new Assembly()
            {
                Name = fileInfo.Filename,
                Filename = fileInfo.Filename,
                FileInfo = fileInfo,
                Deleted = false,
            };

            _assemblyService.Insert(entity);

            //
            return OkMsg();
        }
    }
}