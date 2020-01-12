using Zz.Core.Data.Entity.Users;

namespace Zz.Services.Authentication
{
    public interface IZzAuthenticationService
    {
        bool Authorized();

        string RefreshToken(int expireTime, ExternalUser exUser);

        void SignOut();

        void SignOut(string token);

        ExternalUser GetAuthenticatedUser();
    }
}
