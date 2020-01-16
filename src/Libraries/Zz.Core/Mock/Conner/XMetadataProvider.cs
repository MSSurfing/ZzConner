using Autofac.Engine;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Zz.Core.Data.Entity.Grpc;

namespace Zz.Core.Mock.Conner
{
    /// <summary>
    /// 将Grpc对象生成元数据信息
    /// </summary>
    public class XMetadataProvider : IMetadataProvider
    {
        private const string IGNORE_ASSEMBLY_PATTERN = "^Google.Protobuf|^Grpc.Core";
        private const string IGNORE_MODULE_PATTERN = "^Google.Protobuf|^Grpc.Core";
        private static Type MESSAGE_TYPE = typeof(IMessage);
        private static Type ASYNC_MESSAGE_TYPE = typeof(AsyncUnaryCall<>);

        #region Utilities for type 
        protected virtual bool IsMessageType(ParameterInfo p)
        {
            return MESSAGE_TYPE.IsAssignableFrom(p.ParameterType);
        }

        protected virtual bool IsAsycMessageType(Type type)
        {
            return type.GetGenericTypeDefinition() == ASYNC_MESSAGE_TYPE;
        }

        protected virtual bool IsMatch(string assemblyFullName, string pattern)
        {
            return Regex.IsMatch(assemblyFullName, pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }

        protected virtual bool IgnoreModule(PropertyInfo source)
        {
            // allow types (Map .etc.)
            if (source.PropertyType.Namespace.Equals("Google.Protobuf.Collections", StringComparison.OrdinalIgnoreCase))
                if (source.PropertyType.Name.Equals("MapField`2", StringComparison.OrdinalIgnoreCase)
                    || source.PropertyType.Name.Equals("RepeatedField`1", StringComparison.OrdinalIgnoreCase))
                    return true;

            if (!source.CanWrite)
                return false;

            return IgnoreModule(source.PropertyType);
        }
        protected virtual bool IgnoreModule(Type source)
        {
            return IsMatch(source.Module.Name, IGNORE_MODULE_PATTERN) == false;
        }

        protected virtual bool IgnoreMethod(MethodInfo method)
        {
            return method.IsPublic && method.IsVirtual
                // TODO, not supported async emthod now.
                && method.ReturnType.IsGenericType == false
                && method.DeclaringType.FullName != "System.Object"
                // TODO, use other method to find the real method
                && method.GetParameters().Where(e => e.Name.Equals("headers")).Count() == 1;
        }

        protected virtual DataType ToDataType(Type type)
        {
            var typeCode = Type.GetTypeCode(type);
            if (typeCode == TypeCode.Object)
            {
                if (type.IsGenericType == false)
                    return DataType.Object;

                var genericType = type.GetGenericTypeDefinition();
                if (genericType == typeof(RepeatedField<>))
                {
                    return DataType.RepeatedField;
                }
                else if (genericType == typeof(MapField<,>))
                {
                    return DataType.MapField;
                }
            }

            if (type.IsEnum)
                return DataType.Enum;

            return (DataType)typeCode;
        }
        #endregion

        #region To Model
        protected virtual Property ToPropertyModel(PropertyInfo property)
        {
            var type = property.PropertyType;
            var model = new Property();
            model.Name = property.Name;
            model.AssemblyQualifiedName = type.AssemblyQualifiedName;
            model.DataType = ToDataType(type);
            model.ValueType = Data.Entity.Grpc.ValueType.StaticValue;
            //model.StaticValue = "";
            //model.MinValue = "";
            //model.MaxValue = "";

            return model;
        }

        protected virtual RequestModel ToRequestModel(ParameterInfo property)
        {
            // Todo,支持无参数，待验证
            if (property == null)
                return null;

            var properties = property.ParameterType.GetProperties().Where(IgnoreModule).Select(ToPropertyModel).ToList();
            var model = new RequestModel(properties)
            {
                Name = property.Name,
                AssemblyQualifiedName = property.ParameterType.AssemblyQualifiedName,
            };

            return model;
        }

        protected virtual ResponseModel ToResponseModel(Type returnType)
        {
            if (returnType.IsGenericType && IsAsycMessageType(returnType))
            {
                // Todo 异步消息
                throw new ArgumentException("不支持 AsyncUnaryCall 类型消息！");
            }

            var properties = returnType.GetProperties().Where(IgnoreModule).Select(ToPropertyModel).ToList();
            var model = new ResponseModel(properties)
            {
                Name = returnType.Name,
                AssemblyQualifiedName = returnType.AssemblyQualifiedName,
            };

            return model;
        }

        protected virtual Method ToMethodModel(MethodInfo method)
        {
            var model = new Method();

            model.Name = method.Name;

            // Todo,支持无参数
            model.RequestModel = ToRequestModel(method.GetParameters().FirstOrDefault(IsMessageType));
            model.ResponseModel = ToResponseModel(method.ReturnType);

            return model;
        }

        protected virtual ServiceInfo ToServiceModel(Type clientType)
        {
            // Todo, set AssemblyModel
            var model = new ServiceInfo()
            {
                Name = clientType.Name,
                //Assembly = new Data.Entity.Metadata.Assembly(),
                AssemblyQualifiedName = clientType.AssemblyQualifiedName,
            };

            // Method must be IsPublic && IsVirtual , 暂不支持 异步方法
            var methods = clientType.GetMethods().Where(IgnoreMethod).ToList();
            foreach (var method in methods)
                model.Methods.Add(ToMethodModel(method));

            return model;
        }
        #endregion

        public virtual string GetTypeName(object obj)
        {
            var type = obj.GetType();
            return type.AssemblyQualifiedName;
        }

        public virtual Type GetType(string typeName)
        {
            return Type.GetType(typeName);
        }

        public virtual List<ServiceInfo> GetServices(Assembly assembly)
        {
            var typeFinder = EngineContext.Resolve<ITypeFinder>();
            var clients = typeFinder.FindClassesOfType<ClientBase>(new[] { assembly });

            var serviceList = new List<ServiceInfo>();
            return clients.Select(ToServiceModel).ToList();
        }
    }
}
