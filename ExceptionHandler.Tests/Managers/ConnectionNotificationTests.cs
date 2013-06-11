#region ReSharper Disable

// ReSharper disable InconsistentNaming 
// ReSharper disable CheckNamespace
// ReSharper disable UnusedMember.Global

#endregion

using System.Windows.Forms;
using ExceptionHandler.Mail;
using ExceptionHandler.Managers;
using ExceptionHandler.View;
using Machine.Fakes;
using Machine.Specifications;
using Rhino.Mocks;

namespace ExceptionHandler.Tests.Managers.ConnectionNotificationTests
{
    class ConnectionNotificationSpec : WithFakes
    {
        protected static ConnectionNotifications ConnectionNotifications;
        protected static IMailClient IMailClient;
        protected static IExceptionDialog IExceptionDialog;

        private Establish context = () =>
            {
                IMailClient = An<IMailClient>();
                IExceptionDialog = An<IExceptionDialog>();
                ConnectionNotifications = new ConnectionNotifications(IMailClient, IExceptionDialog);
            };
    }

    [Subject(typeof(ConnectionNotifications), "NotifyConnectionFailed")]
    internal class when_notifying_of_a_connection_failure : ConnectionNotificationSpec
    {
        private Establish context = () =>
        {
            IExceptionDialog.WhenToldTo(x => x.ErrorBox);
            IExceptionDialog.WhenToldTo(x => x.ScopeBox).Return(new RichTextBox());
            IExceptionDialog.WhenToldTo(x => x.ActionBox).Return(new RichTextBox());
            IExceptionDialog.WhenToldTo(x => x.TxtMore).Return(new TextBox());
            IExceptionDialog.WhenToldTo(x => x.Button1).Return(new Button());
            IExceptionDialog.WhenToldTo(x => x.Button2).Return(new Button());
            IExceptionDialog.WhenToldTo(x => x.Button3).Return(new Button());
            IExceptionDialog.WhenToldTo(x => x.PictureBox1).Return(new PictureBox());
        };

        private Because of = () => ConnectionNotifications.NotifyConnectionFailed();

        private It will_send_email = () => IMailClient.WasToldTo(x => x.PrepareAndSend(Arg<MailMessage>.Is.Anything));
        private It will_notify_the_user = () => IExceptionDialog.WasToldTo(x => x.FormShowDialog());
    }

    [Subject(typeof(ConnectionNotifications), "NotifyConnectionSuspended")]
    internal class when_notifying_of_a_suspended_connection : ConnectionNotificationSpec
    {
        private Establish context = () =>
        {
            IExceptionDialog.WhenToldTo(x => x.ErrorBox);
            IExceptionDialog.WhenToldTo(x => x.ScopeBox).Return(new RichTextBox());
            IExceptionDialog.WhenToldTo(x => x.ActionBox).Return(new RichTextBox());
            IExceptionDialog.WhenToldTo(x => x.TxtMore).Return(new TextBox());
            IExceptionDialog.WhenToldTo(x => x.Button1).Return(new Button());
            IExceptionDialog.WhenToldTo(x => x.Button2).Return(new Button());
            IExceptionDialog.WhenToldTo(x => x.Button3).Return(new Button());
            IExceptionDialog.WhenToldTo(x => x.PictureBox1).Return(new PictureBox());
        };

        private Because of = () => ConnectionNotifications.NotifyConnectionSuspended();

        private It will_send_email = () => IMailClient.WasToldTo(x => x.PrepareAndSend(Arg<MailMessage>.Is.Anything));
        private It will_notify_the_user = () => IExceptionDialog.WasToldTo(x => x.FormShowDialog());
    }
}