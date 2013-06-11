#region ReSharper Disable

// ReSharper disable InconsistentNaming 
// ReSharper disable CheckNamespace
// ReSharper disable UnusedMember.Global

#endregion
using System;
using System.Diagnostics;
using ExceptionHandler.Managers;
using Machine.Fakes;
using Machine.Specifications;

namespace ExceptionHandler.Tests.Managers.ExceptionMessageBuilderTests
{
    class ExceptionMessageBuilderSpec : WithFakes
    {
        protected static ExceptionMessageBuilder ExceptionMessageBuilder;
        protected static IAssemblyInfo AssemblyInfo;
        protected static string AnyMessage = "Blargh";

        private Establish context = () =>
            {
                AssemblyInfo = An<IAssemblyInfo>();
                ExceptionMessageBuilder = new ExceptionMessageBuilder(AssemblyInfo);
            };
    }

    [Subject(typeof (ExceptionMessageBuilder), "FormatExceptionForUser")]
    class when_screenshot_is_successful : ExceptionMessageBuilderSpec
    {
        protected static string ScreenshotMessage = "A screenshot was captured and sent to the development team.";

        private It will_inform_the_user =
            () =>
            ExceptionMessageBuilder.FormatExceptionForUser(ScreenshotMessage, AnyMessage, AnyMessage).ShouldContain(
                "screenshot was captured");
    }

    [Subject(typeof (ExceptionMessageBuilder), "FormatExceptionForUser")]
    class when_screenshot_is_not_successful : ExceptionMessageBuilderSpec
    {
        private const string ScreenshotMessage = "A screenshot could NOT be taken.";

        private It will_inform_the_user =
            () =>
            ExceptionMessageBuilder.FormatExceptionForUser(ScreenshotMessage, AnyMessage, AnyMessage).ShouldContain(
                "NOT");
    }

    [Subject(typeof (ExceptionMessageBuilder), "FormatExceptionForUser")]
    class when_logging_to_file_successful : ExceptionMessageBuilderSpec
    {
        private const string LogToFileMessage = "Details were written to a text log at:";

        private It will_inform_the_user =
            () =>
            ExceptionMessageBuilder.FormatExceptionForUser(AnyMessage, LogToFileMessage, AnyMessage).ShouldContain(
                "were written");
    }

    [Subject(typeof (ExceptionMessageBuilder), "FormatExceptionForUser")]
    class when_formatting_exception : ExceptionMessageBuilderSpec
    {
        private const string ExceptionMessage = "Exceptional!";

        private It will_set_the_exception =
            () => ExceptionMessageBuilder.FormatExceptionForUser(AnyMessage, AnyMessage, ExceptionMessage)
                      .ShouldContain("Exceptional");
    }

    [Subject(typeof (ExceptionMessageBuilder), "SystemInfo")]
    class when_an_exception_is_thrown : ExceptionMessageBuilderSpec
    {
        private static Exception Exception;
        private static Version Version;

        private Establish context = () =>
            {
                Exception = new Exception();
                Version = new Version(1, 0, 0, 254);
                AssemblyInfo.WhenToldTo(x => x.FullName).Throw(Exception);
                AssemblyInfo.WhenToldTo(x => x.Version).Return(Version);
            };

        private It will_log_as_much_as_it_can = () => ExceptionMessageBuilder.SystemInfo().ShouldContain(Version.ToString());
    }

    [Subject(typeof (ExceptionMessageBuilder), "EnhancedStackTrace")]
    class when_generating_enhanced_stack_trace : ExceptionMessageBuilderSpec
    {
        private static StackTrace StackTrace;

        private Establish context = () =>
            {
                StackTrace = new StackTrace(true);
            };

        It will_contain_a_stack_trace = () => ExceptionMessageBuilder.EnhancedStackTrace(StackTrace).ShouldContain("Stack Trace");
 
    }

    [Subject(typeof (ExceptionMessageBuilder), "ExceptionInfo")]
    class when_generating_exception_info : ExceptionMessageBuilderSpec
    {
        private static Exception Exception;

        private Establish context = () =>
            {
                Exception = new Exception("", new Exception("inner"));
            };

        It will_contain_an_inner_exception =
            () => ExceptionMessageBuilder.ExceptionInfo(Exception).ShouldContain(Exception.InnerException.ToString());
    }

    [Subject(typeof (ExceptionMessageBuilder), "ExceptionInfo")]
    class when_an_exception_is_thrown_from_method : ExceptionMessageBuilderSpec
    {
        private static Exception Exception;
        private static string Message;

        private Establish context = () =>
            {
                Message = "An exceptional message";
                Exception = new Exception(Message);
                AssemblyInfo.WhenToldTo(x => x.FullName).Throw(Exception);
            };

        It will_log_as_much_as_it_can = () => ExceptionMessageBuilder.ExceptionInfo(Exception).ShouldContain(Message);
    }

    [Subject(typeof(ExceptionMessageBuilder), "BuildEmailbody")]
    internal class when_building_email_body : ExceptionMessageBuilderSpec
    {
        private static string EmailBody;

        private Because of = () => EmailBody = ExceptionMessageBuilder.BuildEmailBody("something happened", "", "", "");
        
        private It will_inform_us_of_what_happened =
            () => EmailBody.ShouldContain("something happened");
        private It will_not_contain_how_user_affected =
            () => EmailBody.ShouldNotContain("How this will affect the user");
        private It will_not_contain_what_the_user_can_do = () => EmailBody.ShouldNotContain("What the user can do");
        private It will_contain_default_more_information = () => EmailBody.ShouldContain("More information:");
    }

    [Subject(typeof(ExceptionMessageBuilder), "BuildEmailBody")]
    internal class when_building_with_more_details : ExceptionMessageBuilderSpec
    {
        private static string EmailBody;

        private Because of =
            () => EmailBody =
            ExceptionMessageBuilder.BuildEmailBody("something happened", "the computer will implode", "run!",
                                                   "more info");
        private It will_contain_how_user_affected = () => EmailBody.ShouldContain("the computer will implode");
        private It will_contain_what_user_can_do = () => EmailBody.ShouldContain("run!");
    }
}