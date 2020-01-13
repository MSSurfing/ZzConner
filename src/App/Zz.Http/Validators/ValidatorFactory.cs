using Autofac.Engine;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zz.Http.Core.Validators
{
    public class ValidatorFactory : IValidatorFactory
    {
        // Todo, 验证实现
        public IValidator GetValidator(Type type)
        {
            if (type != null)
            {
                //var instance = EngineContext.ResolveUnregistered(type);
                //return instance as IValidator;
            }
            return null;
        }

        // Todo, 验证实现
        public IValidator<T> GetValidator<T>()
        {
            var type = typeof(T);
            if (type != null)
            {
                //var instance = EngineContext.ResolveUnregistered(type);
                //return instance as IValidator<T>;
            }
            return null;

        }
    }
}
