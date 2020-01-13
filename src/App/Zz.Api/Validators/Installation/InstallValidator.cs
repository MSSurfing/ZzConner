using FluentValidation;
using Zz.Http.Api.Models.Installation;
using Zz.Http.Core.Validators;

namespace Zz.Http.Api.Validators.Installation
{
    public class InstallValidator : BaseValidator<InstallModel>
    {
        public InstallValidator()
        {
            RuleFor(e => e.AdminEmail).NotEmpty().WithErrorCode("-1");
            RuleFor(e => e.AdminPassword).EmailAddress();
            RuleFor(e => e.AdminPassword).NotEmpty().WithErrorCode("-1");

            RuleFor(e => e.DbConnectionString).NotEmpty().WithErrorCode("-1");
        }
    }
}
