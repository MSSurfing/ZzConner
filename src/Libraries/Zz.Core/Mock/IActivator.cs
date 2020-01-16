using System;
using System.Reflection;

namespace Zz.Core.Mock
{
    public interface IActivator
    {
        object CreateInstance(Type type);

        T CreateInstance<T>();

        #region Load & Unload ( use by AppDomain)

        Assembly LoadInDomain(string dllPath, AppDomain domain = null);

        //T Unwrap<T>();

        AppDomain CreateDomain(string friendlyName = null);

        void Unload(AppDomain domain);
        #endregion
    }
}
