using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace Zz.Http.Core.Mvc
{
    //[ModelBinder(typeof(ComplexTypeModelBinder))]
    public partial class BaseModel
    {
        public virtual void BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {

        }

    }
}
