using Microsoft.EntityFrameworkCore.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zz.Core;
using Zz.Core.Data.Entity.Grpc;

namespace Zz.Services.Grpc
{
    public partial class ServiceInfoService : IServiceInfoService
    {
        #region Constants
        #endregion

        #region Fields
        private readonly IRepository<ServiceInfo> _serviceRepository;
        private readonly IRepository<Method> _methodRepository;
        #endregion

        #region Ctor
        public ServiceInfoService(IRepository<ServiceInfo> serviceRepository, IRepository<Method> method_Repository)
        {
            _serviceRepository = serviceRepository;
            _methodRepository = method_Repository;
        }
        #endregion

        #region Service Methods
        public virtual IPagedList<ServiceInfo> Search(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _serviceRepository.Table;

            query = query.Where(e => !e.Deleted).OrderByDescending(e => e.Id);
            return new PagedList<ServiceInfo>(query, pageIndex, pageSize);
        }

        public virtual ServiceInfo GetById(Guid Id)
        {
            if (Id == Guid.Empty)
                return null;

            return _serviceRepository.GetById(Id); ;
        }

        public virtual void Insert(ServiceInfo entity)
        {
            if (entity == null)
                throw new ArgumentNullException("service");

            _serviceRepository.Insert(entity);
        }

        public virtual void Delete(ServiceInfo entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            entity.Deleted = true;
            _serviceRepository.Update(entity);
        }

        public virtual void Update(ServiceInfo entity)
        {
            if (entity == null)
                throw new ArgumentNullException("service");

            _serviceRepository.Update(entity);
        }
        #endregion

        #region Service method Methods

        public virtual Method GetMethodById(Guid Id)
        {
            if (Id == Guid.Empty)
                return null;

            return _methodRepository.GetById(Id);
        }

        public virtual void DeleteMethod(Method entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _methodRepository.Delete(entity);
        }
        #endregion
    }
}
