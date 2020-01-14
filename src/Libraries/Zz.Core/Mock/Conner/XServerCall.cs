using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zz.Core.Data.Entity.Grpc;

namespace Zz.Core.Mock.Conner
{
    public class XServerCall : IServerCall
    {
        private readonly XActivator _xActivator;

        public XServerCall()
        {
            _xActivator = new XActivator();
        }

        #region Utilities
        protected virtual Channel GetChannel(string host, int port)
        {
            return new Channel(host, port, ChannelCredentials.Insecure);
        }

        protected virtual bool TryGetServiceInstance(Type serviceType, Channel channel, out object instance)
        {
            try
            {
                //var ctor = serviceType.GetConstructor(new[] { typeof(Channel) });
                instance = Activator.CreateInstance(serviceType, new[] { channel });
                return true;
            }
            catch { }

            instance = null;
            return false;
        }
        #endregion

        #region Validate
        protected virtual string ValidResut(ResponseModel model, object result)
        {
            var type = result.GetType();

            // Todo, validate
            var tempMsg = "";
            foreach (var property in model.Properties)
            {
                var propertyInfo = type.GetProperty(property.Name);
                var propertyType = Type.GetType(property.AssemblyQualifiedName);

                var value = propertyInfo.GetValue(result);

                if (property.StaticValue == null || property.StaticValue.Equals(value) == false)
                    tempMsg += $"  {property.Name} != {value}, should be {property.StaticValue}" + Environment.NewLine;
            }

            return tempMsg;
        }
        #endregion

        public virtual object Call(ServiceInfo service, Method method, string host, int port)
        {
            var channel = GetChannel(host, port);

            var serviceType = Type.GetType(service.AssemblyQualifiedName);
            if (TryGetServiceInstance(serviceType, channel, out var instance) == false)
                return null;

            var requestType = Type.GetType(method.RequestModel.AssemblyQualifiedName);
            var requestInstance = _xActivator.CreateInstance(requestType);

            var methodInfo = serviceType.GetMethod(method.Name, new[] { requestType, typeof(Grpc.Core.Metadata), typeof(DateTime), typeof(System.Threading.CancellationToken) });

            methodInfo = serviceType.GetMethod(method.Name, new[] { requestType, typeof(Grpc.Core.Metadata), typeof(DateTime?), typeof(System.Threading.CancellationToken) });


            var result = methodInfo.Invoke(instance, new[] { requestInstance, null, null, null });
            return ValidResut(method.ResponseModel, result);
        }
    }
}
