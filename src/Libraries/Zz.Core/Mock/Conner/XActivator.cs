using Google.Protobuf.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Zz.Core.Mock.Conner
{
    /// <summary>
    /// 反射创建实体，并赋予随机值
    /// 暂仅支持Proto常用字段类型，包括对象、泛型（集合、字典），OneOf。
    /// </summary>
    /// <remarks>
    /// ToImprove, 需要赋值分离
    /// </remarks>
    public class XActivator : IActivator
    {
        #region Fields
        private const string IGNORE_ASSEMBLY_PATTERN = "^Google.Protobuf|^Grpc.Core";
        private const string IGNORE_MODULE_PATTERN = "^Google.Protobuf|^Grpc.Core";
        //[20] = {Google.Protobuf.Collections.RepeatedField`1[System.Int32] Int32List102}

        private readonly IMocker _mocker;
        #endregion

        #region Ctor
        public XActivator()
        {
            _mocker = new SurfMocker();
        }
        #endregion

        #region Utilities for match & ignore module
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
        #endregion

        #region Utilities 2
        protected virtual object GetNormalValue(TypeCode typeCode, Type type)
        {
            switch (typeCode)
            {
                case TypeCode.Int32:
                    if (type.IsEnum)
                        // Todo
                        return _mocker.NextEnum(type);

                    return _mocker.Next();
                case TypeCode.Int64:
                    return _mocker.NextInt64();

                case TypeCode.UInt32:
                    return _mocker.NextUInt32();
                case TypeCode.UInt64:
                    return _mocker.NextUInt64();

                case TypeCode.Boolean:
                    return _mocker.NextBool();
                case TypeCode.String:
                    return _mocker.NextString(10);
                case TypeCode.Double:
                    return _mocker.NextDouble();
                case TypeCode.Single:
                    return _mocker.NextFloat();
                    //case TypeCode.Object:
            }

            // Todo
            return null;// CreateInstance(type);
        }

        protected virtual void SetMapFieldValue(object instance, PropertyInfo property, Type type)
        {
            var keyType = type.GenericTypeArguments[0];
            var valueType = type.GenericTypeArguments[1];

            // Todo, when is a object
            object key = null;
            var typeCode = Type.GetTypeCode(keyType);
            if (typeCode == TypeCode.Object)
            {
                key = CreateInstance(keyType);
            }
            else
            {
                key = GetNormalValue(typeCode, keyType);
            }

            object value = null;
            var typeCode2 = Type.GetTypeCode(valueType);
            if (typeCode2 == TypeCode.Object)
            {
                value = CreateInstance(valueType);
            }
            else
            {
                value = GetNormalValue(typeCode2, valueType);
            }

            var methodInfo = type.GetMethod("Add", new[] { keyType, valueType });
            var propertyValue = property.GetValue(instance);

            methodInfo.Invoke(propertyValue, new[] { key, value });
        }

        protected virtual void SetRepeatedFieldValue(object instance, PropertyInfo property, Type type)
        {
            var keyType = type.GenericTypeArguments[0];

            // Todo, when is a object
            object key = null;
            var typeCode = Type.GetTypeCode(keyType);
            if (typeCode == TypeCode.Object)
            {
                key = CreateInstance(keyType);
            }
            else
            {
                key = GetNormalValue(typeCode, keyType);
            }

            // get Add method & propertyValue from instance
            var methodInfo = type.GetMethod("Add", new[] { keyType });
            var propertyValue = property.GetValue(instance);

            // set propertyValue (from instance)
            methodInfo.Invoke(propertyValue, new[] { key });
        }

        protected virtual void SetValue(object instance, PropertyInfo property)
        {
            var propertyType = property.PropertyType;
            var typeCode = Type.GetTypeCode(propertyType);

            // 值类型 属性赋值
            if (typeCode != TypeCode.Object)
            {
                var value = GetNormalValue(typeCode, propertyType);
                if (value != null)
                {
                    property.SetValue(instance, value);
                }
            }
            else if (propertyType.IsGenericType)
            {
                var genericType = propertyType.GetGenericTypeDefinition();
                if (genericType == typeof(RepeatedField<>))
                {
                    SetRepeatedFieldValue(instance, property, propertyType);
                }
                else if (genericType == typeof(MapField<,>))
                {
                    SetMapFieldValue(instance, property, propertyType);
                }
            }
            else
            {
                // Todo, Set Other Objects
                var value = CreateInstance(propertyType);
                if (value != null)
                    property.SetValue(instance, value);
            }
        }
        #endregion

        public object CreateInstance(Type type)
        {
            var instance = Activator.CreateInstance(type);

            var allProperties = type.GetProperties();
            var properties = allProperties.Where(IgnoreModule).ToList();
            foreach (var property in properties)
            {
                SetValue(instance, property);
            }

            return instance;

        }
        public T CreateInstance<T>()
        {
            return (T)CreateInstance(typeof(T));
        }
    }
}
