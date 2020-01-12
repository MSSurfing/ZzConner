using Zz.Core;
using Zz.Core.Data.Entity.Authentication;

namespace Zz.Services.Authentication
{
    public interface IOpenAuthenticationService
    {
        #region Open Authentication Method
        #endregion

        #region Open Authentication
        IPagedList<OpenAuthentication> Search(string appKey = null
            , int pageIndex = 0, int pageSize = 2147483647); //Int32.MaxValue
        bool Validate(string appKey, string oAuthToken);
        OpenAuthentication GetByAppKey(string appKey);

        void Insert(OpenAuthentication entity);
        void Delete(OpenAuthentication entity);
        void Update(OpenAuthentication entity);
        #endregion
    }
}
