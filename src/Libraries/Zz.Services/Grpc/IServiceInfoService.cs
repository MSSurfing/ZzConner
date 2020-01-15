using System;
using Zz.Core;
using Zz.Core.Data.Entity.Grpc;

namespace Zz.Services.Grpc
{
    public interface IServiceInfoService
    {
        IPagedList<ServiceInfo> Search(int pageIndex = 0, int pageSize = int.MaxValue);

        ServiceInfo GetById(Guid Id);

        void Delete(ServiceInfo entity);

        void Insert(ServiceInfo entity);

        void Update(ServiceInfo entity);

        #region Service method's Methods
        Method GetMethodById(Guid Id);

        void DeleteMethod(Method entity);
        #endregion
    }
}
