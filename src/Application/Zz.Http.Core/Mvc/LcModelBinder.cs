using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Zz.Http.Core.Mvc
{
    public class LcModelBinder : ComplexTypeModelBinder //IModelBinder// DefaultModelBinder
    {
        public LcModelBinder(IDictionary<ModelMetadata, IModelBinder> propertyBinders) : base(propertyBinders)
        {

        }

        //public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        //{
        //    var model = base.BindModel(controllerContext, bindingContext);
        //    if (model is BaseModel)
        //    {
        //        ((BaseModel)model).BindModel(controllerContext, bindingContext);
        //    }
        //    return model;
        //}
    }
}
