using Microsoft.EntityFrameworkCore.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zz.Core;
using Zz.Core.Data.Entity.Users;

namespace Zz.Services.Users
{
    public partial class UserService : IUserService
    {
        #region Constants
        #endregion

        #region Fields
        private readonly IRepository<User> _userRepository;
        #endregion

        #region Ctor
        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        #endregion

        #region Methods
        public IPagedList<User> Search(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _userRepository.Table;

            query = query.Where(e => !e.Deleted).OrderByDescending(e => e.Id);
            return new PagedList<User>(query, pageIndex, pageSize);
        }

        public User GetById(int id)
        {
            if (id == 0)
                return null;

            return _userRepository.GetById(id); ;
        }
        public User GetByMobilephone(string mobilephone)
        {
            if (string.IsNullOrEmpty(mobilephone))
                return null;

            var user = _userRepository.Table.Where(e => e.Mobilephone == mobilephone).FirstOrDefault();
            return user;
        }

        public void Insert(User entity)
        {
            if (entity == null)
                throw new ArgumentNullException("user");

            _userRepository.Insert(entity);
        }

        public void Delete(User entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            entity.Deleted = true;
            _userRepository.Update(entity);
        }

        public void Update(User entity)
        {
            if (entity == null)
                throw new ArgumentNullException("user");

            _userRepository.Update(entity);
        }
        #endregion
    }
}
