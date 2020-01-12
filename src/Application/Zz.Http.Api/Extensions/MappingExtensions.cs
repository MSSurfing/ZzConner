using Zz.Core.Data.Entity.Users;
using Zz.Http.Api.Models.Users;

namespace Zz.Http.Api.Extensions
{
    public static class MappingExtensions
    {
        #region Users
        //User
        public static UserModel ToModel(this User source)
        {
            return new UserModel
            {
                Id = source.Id,
                Name = source.Name,
                Email = source.Email,
                Mobilephone = source.Mobilephone,
            };
        }

        public static User ToEntity(this UserModel source, User destination)
        {
            if (!string.IsNullOrWhiteSpace(source.Name))
                destination.Name = source.Name;
            if (!string.IsNullOrWhiteSpace(source.Email))
                destination.Email = source.Email;

            destination.Mobilephone = source.Mobilephone;

            return destination;
        }
        #endregion
    }
}
