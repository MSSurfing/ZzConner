using Zz.Core.Data.Entity.Users;

namespace Zz.Core
{
    public interface IApiContext
    {
        User CurrentUser { get; set; }
    }
}
