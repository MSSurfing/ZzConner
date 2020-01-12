using System;
using Zz.Core.Data.Entity.Authentication;

namespace Zz.Core.Data.Entity.Users
{
    public partial class ExternalUser : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid OpenAuthenticationId { get; set; }

        public string Identifier { get; set; }

        public string AccessToken { get; set; }

        public virtual User User { get; set; }

        public virtual OpenAuthentication OpenAuthentication { get; set; }
    }
}
