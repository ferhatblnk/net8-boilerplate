using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Core
{
    public class AssemblyContext
    {
        private static string[] allowedAssemblies = new[] {
            "Core",
            "DataAccess",
            "Business",
            "WebAPI",
            "Entities",
            "Migrations.SQLServer"
        };

        private static IReadOnlyList<Assembly> _assemblies;
        private static IReadOnlyList<Assembly> _applicationAssemblies;
        private static readonly ConcurrentDictionary<Type, IReadOnlyList<Type>> _resolvedTypes = new ConcurrentDictionary<Type, IReadOnlyList<Type>>();

        public static IReadOnlyList<T> GetInstancesFromAssembly<T>()
        {
            var types = GetFromAssembly(typeof(T));
            return types.Select(t => (T)Activator.CreateInstance(t)).ToList();
        }

        public static IReadOnlyList<Type> GetFromAssembly<T>()
        {
            return GetFromAssembly(typeof(T));
        }

        public static IReadOnlyList<Type> GetFromAssembly(Type type)
        {
            if (_resolvedTypes.TryGetValue(type, out var resolvedTypes))
                return resolvedTypes;

            var types = GetApplicationAssemblies()
                .SelectMany(assembly => assembly.GetTypes().Where(t => type.IsAssignableFrom(t) && t.IsClass))
                .Distinct()
                .ToList();

            _resolvedTypes.TryAdd(type, types);

            return types;
        }

        public static IReadOnlyList<Assembly> GetAssemblies()
        {
            if (_assemblies != null)
                return _assemblies;

            var assemblyNames = DependencyContext.Default.RuntimeLibraries
                   .SelectMany(library => library.GetDefaultAssemblyNames(DependencyContext.Default));

            var assemblies = new List<Assembly>();
            foreach (var assemblyName in assemblyNames)
                assemblies.Add(LoadAssembly(assemblyName));

            _assemblies = assemblies.AsReadOnly();
            return _assemblies;
        }

        public static IReadOnlyList<Assembly> GetApplicationAssemblies()
        {
            if (_applicationAssemblies != null)
                return _applicationAssemblies;

            var assemblyNames = DependencyContext.Default.RuntimeLibraries
                   .SelectMany(library => library.GetDefaultAssemblyNames(DependencyContext.Default))
                   .Where(assemblyName => allowedAssemblies.Contains(assemblyName.Name));

            var loadedAssemblies = new HashSet<string>();
            var assemblies = new List<Assembly>();
            foreach (var assemblyName in assemblyNames)
            {
                if (loadedAssemblies.Contains(assemblyName.FullName))
                    continue;

                var assembly = LoadAssembly(assemblyName);
                if (assembly == null)
                    continue;

                assemblies.Add(assembly);
                loadedAssemblies.Add(assemblyName.FullName);
            }

            _applicationAssemblies = assemblies.AsReadOnly();
            return _applicationAssemblies;
        }

        private static Assembly LoadAssembly(AssemblyName assemblyName)
        {
            try
            {
                // Try to load the referenced assembly...
                return Assembly.Load(assemblyName);
            }
            catch { }

            return null;
        }
    }
}
