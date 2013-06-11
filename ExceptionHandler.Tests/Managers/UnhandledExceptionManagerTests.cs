#region

using System;
using System.Threading;
using ExceptionHandler.Mail;
using ExceptionHandler.Managers;
using Machine.Fakes;
using Machine.Specifications;
using Rhino.Mocks;

#endregion

#region ReSharper Disable

// ReSharper disable InconsistentNaming 
// ReSharper disable CheckNamespace
// ReSharper disable UnusedMember.Global

#endregion
namespace ExceptionHandler.Tests.Managers.UnhandledExceptionManagerTests
{
    internal class UnhandledExceptionManagerSpec : WithFakes
    {
        protected static UnhandledExceptionManager UnhandledExceptionManager;
        protected static IExceptionNotification IExceptionNotification;
        protected static IAppKiller IAppKiller;

        private Establish context = () =>
            {
                IExceptionNotification = An<IExceptionNotification>();
                IAppKiller = An<IAppKiller>();

                UnhandledExceptionManager = new UnhandledExceptionManager(IExceptionNotification, IAppKiller);
            };

        protected static void VerifyExpectationExpectations()
        {
            IExceptionNotification.WasToldTo(x => x.BuildExceptionMessage(Arg<Exception>.Is.Anything, Arg<string>.Is.Anything));
            IExceptionNotification.WasToldTo(x => x.TakeScreenshot());
            IExceptionNotification.WasToldTo(x => x.WriteToLog());
            IExceptionNotification.WasToldTo(x => x.SendEmail(Arg<MailMessage>.Is.Anything));
            IExceptionNotification.WasToldTo(x => x.CreateAndOpenExceptionUI());
            IAppKiller.WasToldTo(x => x.Kill());
        }
    }

    [Subject(typeof(UnhandledExceptionManager), "Handles UnhandledExceptions")]
    internal class when_calling_unhandled_exception_handler : UnhandledExceptionManagerSpec
    {
        private Because of =
            () =>
            UnhandledExceptionManager.UnhandledExceptionHandler(new object(),
                                                                new UnhandledExceptionEventArgs(new Exception(), true));

        private It will_verify_exception_expectations = () => VerifyExpectationExpectations();
    }

    [Subject(typeof(UnhandledExceptionManager), "Handles ThreadExceptions")]
    internal class when_calling_thread_exception_handler : UnhandledExceptionManagerSpec
    {
        private Because of = () =>
            UnhandledExceptionManager.ThreadExceptionHandler(new object(),
                                                                new ThreadExceptionEventArgs(new Exception()));

        private It will_attempt_a_screenshot = () => IExceptionNotification.WasToldTo(x => x.TakeScreenshot());
        private It will_write_the_exception_to_disc = () => IExceptionNotification.WasToldTo(x => x.WriteToLog());
        private It will_send_an_email = () => IExceptionNotification.WasToldTo(x => x.SendEmail(Arg<MailMessage>.Is.Anything));
        private It will_create_and_open_exception_ui =
            () => IExceptionNotification.WasToldTo(x => x.CreateAndOpenExceptionUI());

        private It will_kill_the_application = () => IAppKiller.WasToldTo(x => x.Kill());
    }
}