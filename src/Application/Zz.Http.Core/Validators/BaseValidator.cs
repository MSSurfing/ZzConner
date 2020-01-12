using FluentValidation;

namespace Zz.Http.Core.Validators
{
    public abstract class BaseValidator<T> : AbstractValidator<T> where T : class
    {
    }
}
