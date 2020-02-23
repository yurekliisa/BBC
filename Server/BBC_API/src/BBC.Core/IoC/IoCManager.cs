using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Core.IoC
{
    public static class IoCManager
    {
        public static IContainer Container;

        public static T GetResolve<T>()
        {
            try
            {
                return Container.Resolve<T>();
            }
            catch (Exception ex)
            {
                //TODO : Log
                throw new Exception("IOC Container: " + typeof(T).Namespace + " " + ex.ToString());
            }
        }

        public static object GetResolve(Type type)
        {
            try
            {
                return Container.Resolve(type);
            }
            catch (Exception ex)
            {
                throw new Exception("IOC Container: " + type.Namespace + " " + ex.ToString());
            }
        }
    }
}
