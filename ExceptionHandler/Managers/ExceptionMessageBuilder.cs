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

        public ExceptionMessageBuilder(IAssemblyInfo assemblyInfo)
        {
            _assemblyInfo = assemblyInfo;
        }

        const string StrBullet = "  •  ";

        public string FormatExceptionForUser(string screenshotMessage, string logToFileMessage, string exceptionMessage)
        {
            return MultiLine(
                "The development team was automatically notified of this problem.",
                "The following information about the error was automatically captured:",
                StrBullet + "An event was written to the application log",
                StrBullet + screenshotMessage,
                StrBullet + logToFileMessage,
                "Detailed error information follows:",
                exceptionMessage
                );
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
                                     new Lazy<string>(() => "Username: " + Environment.UserName + "\n"),
                                     new Lazy<string>(() => "Machine Name: " + Environment.MachineName + "\n"),
                                     new Lazy<string>(() => "IP Address: " + Dns.GetHostEntry(Dns.GetHostName()).AddressList[0] + "\n"),
                                     new Lazy<string>(() => "Application Domain: " + AppDomain.CurrentDomain.FriendlyName + "\n"),
                                     new Lazy<string>(() => "Assembly Codebase: " + _assemblyInfo.CodeBase + "\n"),
                                     new Lazy<string>(() => "Assembly Full Name: " + _assemblyInfo.FullName + "\n"),
                                     new Lazy<string>(() => "Assembly Version: " + _assemblyInfo.Version + "\n")
                                 });
        }

        private Lazy<string> addHeader(string header)
        {
            var msg = "\n---- " + header + "----\n\n";
            return new Lazy<string>(() => msg);
        }

        public string ExceptionInfo(Exception exception)
        {
            return MultiLazy(new List<Lazy<string>>
                                 {
                                     new Lazy<string>(() => exception.Message + "\n"),
                                     addHeader("Exception Info"),
                                     new Lazy<string>(() => "Exception Source: " + exception.Source + "\n"),
                                     new Lazy<string>(() => "Exception Type: " + exception.GetType().FullName + "\n"),
                                     new Lazy<string>(() => "Exception Message: " + exception.Message + "\n"),
                                     new Lazy<string>(() => "Exception Target Site: " + ((exception.TargetSite != null) ? exception.TargetSite.Name : "") + "\n"),
                                     addHeader("System Info"),
                                     new Lazy<string>(SystemInfo),
                                     addHeader("StackTrace Info"),
                                     new Lazy<string>(() => exception.StackTrace),
                                     new Lazy<string>(() => "\n"),
                                     new Lazy<string>(() => "\n"),
                                     new Lazy<string>(() => "(Inner Exception)"),
                                     new Lazy<string>(() => "\n"),
                                     new Lazy<string>(() => exception.InnerException.ToString()),
                                     new Lazy<string>(() => "\n"),
                                     new Lazy<string>(() => "\n"),
                                     addHeader("Old Stuff"),
                                     new Lazy<string>(() => "(Enhanced Stack Trace)"),
                                     new Lazy<string>(() => "\n"),
                                     new Lazy<string>(() => EnhancedStackTrace(new StackTrace(true))),
                                     new Lazy<string>(() => "\n"),
                                     new Lazy<string>(() => "\n")
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