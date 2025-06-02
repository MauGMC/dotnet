using System.Reflection;
using System.Runtime.Loader;

namespace Presentacion.Servicios
{
    public class CustomAssemblyLoadContext : AssemblyLoadContext
    {
        public IntPtr LoadUnmanagedLibrary(string absolutePath) => LoadUnmanagedDll(absolutePath);
        protected override nint LoadUnmanagedDll(string unmanagedDllName) =>
            LoadUnmanagedDllFromPath(unmanagedDllName);
        protected override Assembly Load(AssemblyName assemblyName) => null;

    }
}
