#region

using System;
using System.Reflection;
using ExceptionHandler.Mail;
using Machine.Fakes;
using Machine.Specifications;

#endregion

#region ReSharper Disable

// ReSharper disable InconsistentNaming 
// ReSharper disable CheckNamespace
// ReSharper disable UnusedMember.Global

#endregion
namespace ExceptionHandler.Tests.Mail.MailClientTests
{
    internal class MailClientSpec : WithFakes
    {
        protected static IPostalWorker IPostalWorker;
        protected static MailClient MailClient;
        protected static string ExceptionFreeAttachmentPath;

        private Establish context = () =>
                                        {
                                            ExceptionFreeAttachmentPath = Assembly.GetExecutingAssembly().Location;
                                            IPostalWorker = An<IPostalWorker>();
                                            MailClient = new MailClient(IPostalWorker);
                                        };
    }

    [Subject(typeof (MailClient), "PrepareAndSend")]
    internal class when_sending_mail : MailClientSpec
    {
        private static MailMessage MailMessage;

        private Establish context = () =>
                                        {
                                            MailMessage = new MailMessage();
                                            MailMessage.AttachmentPaths.Add(ExceptionFreeAttachmentPath);
                                        };

        private Because of = () => MailClient.PrepareAndSend(MailMessage);

        private It will_tell_the_postal_worker_to_send_mail =
            () => IPostalWorker.WasToldTo(x => x.SendMail(MailMessage));
    }

    [Subject(typeof (MailClient), "PrepareAndSend")]
    internal class when_at_first_you_dont_succeed : MailClientSpec
    {
        private static MailMessage MailMessage;

        private Establish context = () =>
                                        {
                                            MailMessage = new MailMessage();
                                            MailMessage.AttachmentPaths.Add(ExceptionFreeAttachmentPath);
                                            IPostalWorker.WhenToldTo(x => x.SendMail(MailMessage)).Throw(new Exception());
                                        };

        private Because of = () => Catch.Exception(() => MailClient.PrepareAndSend(MailMessage));

        private It will_try_try_again =
            () => IPostalWorker.WasToldTo(x => x.SendMail(MailMessage)).Times(5);
    }
}