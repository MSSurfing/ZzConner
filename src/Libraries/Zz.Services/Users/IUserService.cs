using System;
using System.Collections.Generic;
using System.Text;
using Zz.Core;
using Zz.Core.Data.Entity.Users;

namespace Zz.Services.Users
{
    public partial interface IUserService
    {
        IPagedList<User> Search(int pageIndex = 0, int pageSize = int.MaxValue);

        User GetById(int id);

        User GetByMobilephone(string mobilephone);

        void Delete(User entity);

        void Insert(User entity);

        void Update(User entity);
    }
}
