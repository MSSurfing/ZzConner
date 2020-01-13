using Zz.Http.Core.Mvc;

namespace Zz.Http.Api.Models.Installation
{
    public class InstallModel : BaseModel
    {
        #region Administrator attributes
        public string AdminEmail { get; set; }
        public string AdminPassword { get; set; }
        #endregion

        #region Database attributes
        public string DbType { get; set; }
        public string DbConnectionString { get; set; }

        public bool CreateDatabaseIfNotExists { get; set; }
        public bool UseSampleData { get; set; }
        #endregion
    }
}
