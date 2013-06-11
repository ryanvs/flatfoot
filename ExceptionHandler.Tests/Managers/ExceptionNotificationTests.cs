#region ReSharper Disable

// ReSharper disable InconsistentNaming 
// ReSharper disable CheckNamespace
// ReSharper disable UnusedMember.Global

#endregion

using ExceptionHandler.Mail;
using ExceptionHandler.Managers;
using ExceptionHandler.Tests.Managers.UnhandledExceptionManagerTests;
using Machine.Fakes;
using Machine.Specifications;

namespace ExceptionHandler.Tests.Managers.ExceptionNotificationTests
{
    class ExceptionNotificationSpec : WithFakes
    {
        protected static ExceptionNotification ExceptionNotification;
        protected static IExceptionMessageBuilder IExceptionMessageBuilder;
        protected static IMailClient IMailClient;

        private Establish context = () =>
            {
                IExceptionMessageBuilder = An<IExceptionMessageBuilder>();
                IMailClient = An<IMailClient>();
                ExceptionNotification = new ExceptionNotification(IExceptionMessageBuilder, IMailClient);
            };
    }

    [Subject(typeof(ExceptionNotification), "ExceptionToEmail attachments")]
    internal class when_sending_email : ExceptionNotificationSpec
    {
        private static MailMessage MailMessage;
        private const string PathToScreenshot = "/path/to/screenshot.png";
        private Establish context = () =>
        {
            ExceptionNotification.ScreenshotFullPath = PathToScreenshot;
            MailMessage = new MailMessage();
        };

        private Because of = () => ExceptionNotification.SendEmail(MailMessage);

        private It will_attach_the_screenshot = () => MailMessage.AttachmentPaths[0].ShouldContain(PathToScreenshot);
        private It will_attach_the_log_file = () => MailMessage.AttachmentPaths[1].ShouldContain("UnhandledExceptionLog");

        private It will_send_the_email = () => IMailClient.WasToldTo(x => x.PrepareAndSend(MailMessage));
    }
}
