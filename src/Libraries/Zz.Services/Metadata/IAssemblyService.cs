using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zz.Core;
using Zz.Core.Data.Entity.Metadata;

namespace Zz.Services.Metadata
{
    public interface IAssemblyService
    {
        IPagedList<Assembly> Search(int pageIndex = 0, int pageSize = 2147483647);

        Assembly GetById(Guid Id);

        void Delete(Assembly entity);

        void Insert(Assembly entity);

        void Update(Assembly entity);

        #region 

        #endregion
    }
}
