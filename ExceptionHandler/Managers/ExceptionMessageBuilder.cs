using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;

namespace ExceptionHandler.Managers
{
    public class ExceptionMessageBuilder : IExceptionMessageBuilder
    {
        readonly IAssemblyInfo _assemblyInfo;

        Dictionary<string, Func<string>> _replaceStrings;

        public ExceptionMessageBuilder(IAssemblyInfo assemblyInfo)
        {
            _assemblyInfo = assemblyInfo;
        }

        const string StrBullet = "  •  ";

        /// <summary>
        /// Replace generic constants in strings with specific values 
        /// </summary>
        public string ReplaceStringVals(string input)
        {
            if (_replaceStrings == null)
            {
                _replaceStrings = new Dictionary<string, Func<string>>();

                // Get application product name
                _replaceStrings["(app)"] = () => _assemblyInfo.ProductName;

                // Only add contact info if specified
                string contact = System.Configuration.ConfigurationManager.AppSettings.Get("UnhandledExceptionManager/ContactInfo") as string;
                if (!string.IsNullOrEmpty(contact))
                    _replaceStrings["(contact)"] = () => contact;
            }

            string output = input ?? "";
            foreach (string key in _replaceStrings.Keys)
            {
                string value = _replaceStrings[key]();
                output = output.Replace(key, value);
            }
            return output;
        }

        public string FormatExceptionForUser(string screenshotMessage, string logToFileMessage, string exceptionMessage)
        {
            var result = MultiLine(
                "The development team was automatically notified of this problem. If you need immediate assistance, contact (contact).",
                "",
                "The following information about the error was automatically captured:",
                "",
                StrBullet + "An event was written to the application log",
                StrBullet + screenshotMessage,
                StrBullet + logToFileMessage,
                "",
                "Detailed error information follows:",
                "",
                exceptionMessage
                );
            ReplaceStringVals(result);
            return result;
        }

        static string MultiLine(params string[] args)
        {
            return string.Join(Environment.NewLine, args);
        }

        static string MultiLazy(IEnumerable<Lazy<string>> args)
        {
            var stringBuilder = new StringBuilder();
            foreach (var arg in args)
            {
                try
                {
                    stringBuilder.Append(arg.Value);
                }
// ReSharper disable EmptyGeneralCatchClause -- we want all the information we can get.
                catch (Exception) {}
// ReSharper restore EmptyGeneralCatchClause
            }
            return stringBuilder.ToString();
        }

        public string SystemInfo()
        {
            return MultiLazy(new List<Lazy<string>>
                                 {
                                     new Lazy<string>(() => "Date and Time: " + DateTime.Now.ToString() + "\r\n"),
                                     new Lazy<string>(() => "Username: " + Environment.UserName + "\r\n"),
                                     new Lazy<string>(() => "Machine Name: " + Environment.MachineName + "\r\n"),
                                     new Lazy<string>(() => "IP Address: " + Dns.GetHostEntry(Dns.GetHostName()).AddressList[0] + "\r\n"),
                                     new Lazy<string>(() => "Application Domain: " + AppDomain.CurrentDomain.FriendlyName + "\r\n"),
                                     new Lazy<string>(() => "Assembly Codebase: " + _assemblyInfo.CodeBase + "\r\n"),
                                     new Lazy<string>(() => "Assembly Full Name: " + _assemblyInfo.FullName + "\r\n"),
                                     new Lazy<string>(() => "Assembly Version: " + _assemblyInfo.Version + "\r\n"),
                                     new Lazy<string>(() => "Assembly Build Date: " + _assemblyInfo.BuildDate.ToString() + "\r\n"),
                                 });
        }

        private Lazy<string> addHeader(string header)
        {
            var msg = "\r\n---- " + header + "----\r\n\r\n";
            return new Lazy<string>(() => msg);
        }

        public string ExceptionInfo(Exception exception)
        {
            var builder = new StringBuilder();
            return MultiLazy(new List<Lazy<string>>
                                 {
                                     new Lazy<string>(() => exception.Message + "\r\n"),
                                     addHeader("Exception Info"),
                                     new Lazy<string>(() => "Exception Source: " + exception.Source + "\r\n"),
                                     new Lazy<string>(() => "Exception Type: " + exception.GetType().FullName + "\r\n"),
                                     new Lazy<string>(() => "Exception Message: " + exception.Message + "\r\n"),
                                     new Lazy<string>(() => "Exception Target Site: " + ((exception.TargetSite != null) ? exception.TargetSite.Name : "") + "\r\n"),
                                     addHeader("System Info"),
                                     new Lazy<string>(SystemInfo),
                                     addHeader("StackTrace Info"),
                                     new Lazy<string>(() => exception.StackTrace),
                                     new Lazy<string>(() => "\r\n"),
                                     new Lazy<string>(() => "\r\n"),
                                     new Lazy<string>(() => "(Inner Exception)"),
                                     new Lazy<string>(() => "\r\n"),
                                     new Lazy<string>(() => exception.InnerException.ToString()),
                                     new Lazy<string>(() => "\r\n"),
                                     new Lazy<string>(() => "\r\n"),
                                     addHeader("Old Stuff"),
                                     new Lazy<string>(() => "(Enhanced Stack Trace)"),
                                     new Lazy<string>(() => "\r\n"),
                                     new Lazy<string>(() => EnhancedStackTrace(new StackTrace(true))),
                                     new Lazy<string>(() => "\r\n"),
                                     new Lazy<string>(() => "\r\n")
                                 }
                );
        }

        public string EnhancedStackTrace(StackTrace trace)
        {
            var enhancedTrace = new StringBuilder();
            enhancedTrace.Append("---- Stack Trace ----");
            enhancedTrace.Append(Environment.NewLine);
            for (var frame = 0; frame <= trace.FrameCount - 1; frame++)
                enhancedTrace.Append(StackFrameToString(trace.GetFrame(frame)));
            return enhancedTrace.ToString();
        }

        private string StackFrameToString(StackFrame frame)
        {
            return MultiLine(
                "   " + DeclaringTypeInformation(frame),
                String.Join<ParameterInfo>(",", frame.GetMethod().GetParameters()),
                "       " + SourceCodeInformation(frame)
                );
        }

        private string SourceCodeInformation(StackFrame frame)
        {
            if (String.IsNullOrEmpty(frame.GetFileName()))
                return Path.GetFileName(_assemblyInfo.CodeBase) + ": N " + frame.GetNativeOffset();
            return Path.GetFileName(frame.GetFileName()) + ": line " + frame.GetFileLineNumber()
               + ", col " + frame.GetFileColumnNumber() + ", IL " + frame.GetILOffset();
        }

        private string DeclaringTypeInformation(StackFrame frame)
        {
            MemberInfo memberInfo = frame.GetMethod();
            var declaringType = memberInfo.DeclaringType;
            if (declaringType == null) return "";
            return declaringType.Namespace + "." + declaringType.Name + "." + memberInfo.Name;
        }

        public string BuildEmailBody(string whatHappened, string howUserAffected, string whatUserCanDo, string moreDetails)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("What happened: " + whatHappened);
            stringBuilder.Append(Environment.NewLine);
            if (!String.IsNullOrEmpty(howUserAffected))
            {
                stringBuilder.Append("How this will affect the user: " + howUserAffected);
                stringBuilder.Append(Environment.NewLine);
            }
            if (!String.IsNullOrEmpty(whatUserCanDo))
            {
                stringBuilder.Append("What the user can do: " + whatUserCanDo);
                stringBuilder.Append(Environment.NewLine);
            }
            stringBuilder.Append("More information: " + moreDetails);
            return stringBuilder.ToString();
        }
    }
}