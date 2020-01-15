using Microsoft.EntityFrameworkCore.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zz.Core;
using Zz.Core.Data.Entity.Metadata;

namespace Zz.Services.Metadata
{
    public class AssemblyService : IAssemblyService
    {
        #region Constants
        #endregion

        #region Fields
        private readonly IRepository<Assembly> _assemblyRepository;
        #endregion

        #region Ctor
        public AssemblyService(IRepository<Assembly> serviceRepository)
        {
            _assemblyRepository = serviceRepository;
        }
        #endregion

        #region Service Methods
        public virtual IPagedList<Assembly> Search(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _assemblyRepository.Table;

            query = query.Where(e => !e.Deleted).OrderByDescending(e => e.Id);
            return new PagedList<Assembly>(query, pageIndex, pageSize);
        }

        public virtual Assembly GetById(Guid Id)
        {
            if (Id == Guid.Empty)
                return null;

            return _assemblyRepository.GetById(Id); ;
        }

        public virtual void Insert(Assembly entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _assemblyRepository.Insert(entity);
        }

        public virtual void Delete(Assembly entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            entity.Deleted = true;
            _assemblyRepository.Update(entity);
        }

        public virtual void Update(Assembly entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _assemblyRepository.Update(entity);
        }
        #endregion
    }
}
