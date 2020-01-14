using Zz.Core.Data.Entity.Grpc;

namespace Zz.Core.Mock
{
    public interface IServerCall
    {
        object Call(ServiceInfo service, Method method, string host, int port);
    }
}
