using Microsoft.EntityFrameworkCore.Engine;
using System;
using System.Linq;
using Zz.Core.Data.Entity.Users;
using Zz.Services.Authentication;

namespace Zz.Services.Users.External
{
    public partial class ExternalUserService : IExternalUserService
    {
        #region Constants
        #endregion

        #region Fields
        private readonly IRepository<ExternalUser> _externalUserRepository;
        private readonly IOpenAuthenticationService _openAuthenticationService;
        #endregion

        #region Ctor
        public ExternalUserService(IRepository<ExternalUser> externalUserRepository,
            IOpenAuthenticationService openAuthenticationService)
        {
            _externalUserRepository = externalUserRepository;
            _openAuthenticationService = openAuthenticationService;
        }
        #endregion

        #region Methods
        public virtual ExternalUser GetById(Guid Id)
        {
            if (Id == Guid.Empty)
                return null;

            return _externalUserRepository.GetById(Id);
        }
        public virtual ExternalUser GetByIdentifier(string appKey, string identifier)
        {
            var entity = _externalUserRepository.Table
                .FirstOrDefault(o => o.Identifier == identifier
                    && o.OpenAuthentication.AppKey == appKey);
            return entity;
        }

        public virtual ExternalUser GetByUserId(string appKey, Guid userId)
        {
            if (userId == Guid.Empty) return null;
            var entity = _externalUserRepository.Table
                .FirstOrDefault(o => o.UserId == userId &&
                    o.OpenAuthentication.AppKey == appKey);
            return entity;
        }

        public virtual void Insert(ExternalUser entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _externalUserRepository.Insert(entity);
        }

        public virtual void Update(ExternalUser entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _externalUserRepository.Update(entity);
        }

        public virtual void Delete(ExternalUser entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            entity.User.Deleted = true;
            Update(entity);

            this._externalUserRepository.Delete(entity);
        }
        #endregion
    }
}
