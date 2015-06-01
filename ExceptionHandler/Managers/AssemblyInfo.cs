using System;
using System.Reflection;
using System.Text.RegularExpressions;

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

            ProductName = GetProductName(_assembly);
            BuildDate = AssemblyBuildDate(_assembly, Version);

            //-- for non-web hosted apps, returns:
            //--   "[path]\bin\YourAssemblyName."
            //-- for web hosted apps, returns URL with non-filesystem chars removed:
            //--   "c:\http___domain\path\YourAssemblyName."
            if (CodeBase.StartsWith("http://"))
                ApplicationPath = @"c:\" + Regex.Replace(CodeBase, @"[\/\\\:\*\?\""\<\>\|]", "_") + ".";
            else
                ApplicationPath = System.AppDomain.CurrentDomain.BaseDirectory + System.AppDomain.CurrentDomain.FriendlyName + ".";
        }

        public string FullName { get; private set; }
        public string CodeBase { get; private set; }
        public string ApplicationPath { get; private set; }
        public string ProductName { get; private set; }
        public Version Version { get; private set; }
        public DateTime BuildDate { get; private set; }

        /// <summary>
        /// for non-web hosted apps, returns:
        ///   "[path]\bin\YourAssemblyName."
        /// for web hosted apps, returns URL with non-filesystem chars removed:
        ///   "c:\http___domain\path\YourAssemblyName."
        /// </summary>
        private static string GetApplicationPath(string codeBase)
        {
            if (codeBase != null && codeBase.StartsWith("http://"))
                return @"c:\" + Regex.Replace(codeBase, @"[\/\\\:\*\?\""\<\>\|]", "_") + ".";
            else
                return System.AppDomain.CurrentDomain.BaseDirectory + System.AppDomain.CurrentDomain.FriendlyName + ".";

        }

        /// <summary>
        ///
        /// returns build datetime of assembly
        /// assumes default assembly value in AssemblyInfo:
        /// <Assembly: AssemblyVersion("1.0.*")> 
        /// 
        /// filesystem create time is used, if revision and build were overridden by user
        /// </summary>
        private static DateTime AssemblyBuildDate(System.Reflection.Assembly assembly, Version version, bool forceFileDate = false)
        {
            DateTime result;
            if (forceFileDate)
                result = AssemblyFileTime(assembly);
            else
            {
                result = DateTime.Parse("2000-01-01")
                    .AddDays(version.Build)
                    .AddSeconds(version.Revision * 2);
                if (TimeZone.IsDaylightSavingTime(DateTime.Now, TimeZone.CurrentTimeZone.GetDaylightChanges(DateTime.Now.Year)))
                    result = result.AddHours(1);
                if (result > DateTime.Now || version.Build < 730 || version.Revision == 0)
                    result = AssemblyFileTime(assembly);
            }
            return result;
        }

        /// <summary>
        /// exception-safe file attrib retrieval; we don't care if this fails
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        private static DateTime AssemblyFileTime(System.Reflection.Assembly assembly)
        {
            try
            {
                return System.IO.File.GetLastWriteTime(assembly.Location);
            }
            catch
            {
                return DateTime.MaxValue;
            }
        }

        /// <summary>
        /// Initializes the Product property.
        /// </summary>
        static string GetProductName(System.Reflection.Assembly assembly)
        {
            string result = "";

            try
            {
                object[] customAttributes = assembly.GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if ((customAttributes != null) && (customAttributes.Length > 0))
                    result = ((AssemblyProductAttribute)customAttributes[0]).Product;
            }
            catch { }

            return result;
        }

    }
}