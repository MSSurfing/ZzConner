using Zz.Core.Data.Entity.Grpc;

namespace Zz.Core.Data
{
    public static class TableNames
    {
        public const string UserTable = "User";
        public const string ExternalUserMapping = "OpenAuthentication_User_Mapping";

        public const string ServiceInfoTable = nameof(ServiceInfo);
        public const string MethodTable = nameof(Method);
        public const string PropertyTable = nameof(Property);

        public const string RequestModelTable = nameof(RequestModel);
        public const string ResponseModelTable = nameof(ResponseModel);
    }
}
