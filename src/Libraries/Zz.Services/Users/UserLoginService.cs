using Zz.Core.Data.Entity.Users;

namespace Zz.Services.Users
{
    public partial class UserLoginService : IUserLoginService
    {
        #region Fields
        private readonly IUserService _userService;
        #endregion

        #region Ctor
        public UserLoginService(IUserService userService)
        {
            _userService = userService;
        }
        #endregion

        public LoginResult Validate(string mobilephone, string password)
        {
            var entity = _userService.GetByMobilephone(mobilephone);

            if (entity == null)
                return LoginResult.NotExist;
            if (entity.Deleted)
                return LoginResult.Deleted;
            if (!entity.Active)
                return LoginResult.NotActive;

            var pwd = "";
            //switch (entity.PasswordFormat)
            //{
            //    default:
            //        pwd = password;
            //        break;
            //}

            pwd = password;
            bool isValid = pwd == entity.Password;
            if (!isValid)
                return LoginResult.WrongPassword;

            return LoginResult.Successful;
        }
    }
}
