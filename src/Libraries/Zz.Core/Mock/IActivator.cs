using System;

namespace Zz.Core.Mock
{
    public interface IActivator
    {
        object CreateInstance(Type type);

        T CreateInstance<T>();
    }
}
