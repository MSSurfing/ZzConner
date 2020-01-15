using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zz.Http.Core.Mvc;

namespace Zz.Api.Models.Assemblies
{
    public class AssemblyModel : BaseEntityModel
    {
        public string Name { get; set; }
        public string Filename { get; set; }
    }
}
