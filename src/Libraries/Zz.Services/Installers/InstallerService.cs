using Microsoft.EntityFrameworkCore.Engine;
using System;
using System.Linq;
using Zz.Core.Data;
using Zz.Core.Data.Entity.Users;

namespace Zz.Services.Installers
{
    public partial class InstallerService : IInstallerService
    {
        #region Fields
        private readonly IRepository<User> _userRepository;
        private readonly IDbContext _dbContext;
        #endregion

        #region Ctor
        public InstallerService(IRepository<User> userRepository,
            IDbContext dbContext)
        {
            this._userRepository = userRepository;
            this._dbContext = dbContext;
        }
        #endregion

        public virtual void InitializeDatabase()
        {
            const string tableName = TableNames.UserTable;
            var exists = _dbContext.QueryFromSql<StringValue>($"SELECT name AS Value FROM [sysobjects] WHERE name = '{tableName}'");
            if (exists.Count() >= 1)
                return;

            var createStripts = _dbContext.DatabaseCreateScript();
            if (!string.IsNullOrEmpty(createStripts))
            {
                var command = createStripts.Replace("GO", "");
                _dbContext.ExecuteSqlCommand(command);
            }
        }

        public virtual void InitializeSampleData()
        {
            //Todo
        }

        public virtual void InstallManager(string username, string password)
        {
            if (_userRepository.TableNoTracking.Where(e => e.Name == username).Count() > 0)
                return;

            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = username,
                Mobilephone = username,
                Password = password,
                CreatedOn = DateTime.Now,
                Active = true,
                Deleted = false,
            };

            _userRepository.Insert(user);
        }
    }
}
