using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BBC.Core.Kernel
{
    public static class KernelAssembly
    {
        private static List<Assembly> Assemblies = new List<Assembly>();

        public static List<Type> GetAssemblyType(Type type = null)
        {
            List<Type> types = new List<Type>();
            foreach (var item in Assemblies)
            {
                types.AddRange(item.GetTypes().ToList());
            }
            if (type != null)
                types = types.Where(x => x.GetType() == type.GetType()).ToList();
            return types;
        }

        public static List<Assembly> GetAssemblies() => Assemblies;

        public static void SetAssembly(Assembly assembly)
        {
            if (!Assemblies.Contains(assembly))
            {
                Assemblies.Add(assembly);
            }
        }

        public static void SetAssembly(List<Assembly> assemblies)
        {
            foreach (var assembly in assemblies)
            {
                SetAssembly(assembly);
            }
        }
    }
}
