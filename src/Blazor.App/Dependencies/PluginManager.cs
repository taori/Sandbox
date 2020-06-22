using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace Blazor.App.Dependencies
{
    public class PluginManager
    {
        public IEnumerable<Assembly> GetPlugins()
        {
            var dlls = Directory.EnumerateFiles(@"D:\GitHub\Sandbox\src\Blazor.App", "*.dll", SearchOption.AllDirectories);
            var filtered = dlls
                .Where(d => d.EndsWith("Blazor.Plugin1.dll", StringComparison.OrdinalIgnoreCase))
                .ToArray();
            foreach (var file in filtered)
            {
                yield return AssemblyLoadContext.Default.LoadFromAssemblyPath(file);
            }
        }
    }
}