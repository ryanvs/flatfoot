using System.Reflection;

namespace ExceptionHandler.Managers
{
    public class AssemblyInfo : IAssemblyInfo
    {
        readonly Assembly _assembly;

        public AssemblyInfo()
        {
            _assembly = Assembly.GetEntryAssembly() ?? Assembly.GetCallingAssembly();
            FullName = _assembly.FullName;
            CodeBase = _assembly.CodeBase;
            Version = _assembly.GetName().Version;
        }

        public string FullName { get; private set; }
        public string CodeBase { get; private set; }
        public System.Version Version { get; private set; }
    }
}