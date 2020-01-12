using Microsoft.EntityFrameworkCore.Engine;
using System;
using System.Linq;
using Zz.Core;
using Zz.Core.Data.Entity.Authentication;

namespace Zz.Services.Authentication
{
    public class OpenAuthenticationService : IOpenAuthenticationService
    {
        private readonly IRepository<OpenAuthentication> _openAuthenticationRepository;

        #region Ctor
        public OpenAuthenticationService(IRepository<OpenAuthentication> openAuthenticationRepository)
        {
            _openAuthenticationRepository = openAuthenticationRepository;
        }
        #endregion

        public bool Validate(string appKey, string oAuthToken)
        {
            var open = GetByAppKey(appKey);

            if (open != null && open.OAuthToken == oAuthToken)
                return true;

            return false;
        }

        public virtual IPagedList<OpenAuthentication> Search(string appKey = null
             , int pageIndex = 0, int pageSize = 2147483647)
        {
            var query = _openAuthenticationRepository.Table;
            if (string.IsNullOrEmpty(appKey))
                query = query.Where(e => e.AppKey == appKey);

            query = query.OrderByDescending(c => c.AppKey);

            return new PagedList<OpenAuthentication>(query, pageIndex, pageSize);
        }

        public virtual OpenAuthentication GetByAppKey(string appKey)
        {
            if (string.IsNullOrWhiteSpace(appKey))
                return null;

            var query = from c in _openAuthenticationRepository.Table
                        where c.AppKey == appKey
                        orderby c.Id
                        select c;
            var entity = query.FirstOrDefault();
            return entity;
        }

        public virtual void Insert(OpenAuthentication entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _openAuthenticationRepository.Insert(entity);
        }

        public virtual void Update(OpenAuthentication entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _openAuthenticationRepository.Update(entity);
        }

        public virtual void Delete(OpenAuthentication entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _openAuthenticationRepository.Delete(entity);
        }
    }
}
