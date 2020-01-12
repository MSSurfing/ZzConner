using Zz.Core.Data.Entity.Users;

namespace Zz.Services.Users
{
    public partial interface IUserLoginService
    {
        LoginResult Validate(string mobilephone, string password);
    }
}
