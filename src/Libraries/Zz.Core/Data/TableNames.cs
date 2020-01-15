using Zz.Core.Data.Entity.Grpc;
using Zz.Core.Data.Entity.Media;
using Zz.Core.Data.Entity.Metadata;

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

        public const string FileInfoTable = nameof(FileInfo);
        public const string AssemblyTable = nameof(Assembly);
    }
}
