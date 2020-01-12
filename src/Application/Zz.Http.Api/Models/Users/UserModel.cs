using Zz.Http.Core.Mvc;

namespace Zz.Http.Api.Models.Users
{
    public class UserModel : BaseEntityModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobilephone { get; set; }
    }
}
