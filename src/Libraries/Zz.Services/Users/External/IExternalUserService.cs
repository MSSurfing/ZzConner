using System;
using Zz.Core.Data.Entity.Users;

namespace Zz.Services.Users.External
{
    public interface IExternalUserService
    {
        ExternalUser GetByUserId(string appKey, Guid customerId);

        ExternalUser GetByIdentifier(string appKey, string identifier);

        ExternalUser GetById(Guid Id);

        void Insert(ExternalUser externalCustomer);

        void Update(ExternalUser externalCustomer);

        void Delete(ExternalUser externalCustomer);
    }
}
