#region

using System;
using System.Windows.Forms;
using ExceptionHandler.Mail;
using ExceptionHandler.Managers;
using ExceptionHandler.View;
using Machine.Fakes;
using Machine.Specifications;
using Rhino.Mocks;

#endregion

#region ReSharper Disable

// ReSharper disable InconsistentNaming 
// ReSharper disable CheckNamespace
// ReSharper disable UnusedMember.Global

#endregion
namespace ExceptionHandler.Tests.Managers.HandledExceptionManagerTests
{
    internal class HandledExceptionManagerSpec : WithFakes
    {
        protected static HandledExceptionManager HandledExceptionManager;
        protected static IMailClient IMailClient;
        protected static IExceptionMessageBuilder IExceptionMessageBuilder;
        protected static IExceptionDialog IExceptionDialog;
        private Establish context = () =>
            {
                IMailClient = An<IMailClient>();
                IExceptionMessageBuilder = An<IExceptionMessageBuilder>();
                IExceptionDialog = An<IExceptionDialog>();
                HandledExceptionManager = new HandledExceptionManager(IMailClient, IExceptionMessageBuilder, IExceptionDialog);
            };
    }

    [Subject(typeof(HandledExceptionManager), "Creating and sending email")]
    internal class when_creating_email : HandledExceptionManagerSpec
    {
        private Because of = () => HandledExceptionManager.SendNotificationEmail("x", "y", "z", "a");

        private It will_build_from_the_message_builder =
            () => IExceptionMessageBuilder.WasToldTo(
                x => x.BuildEmailBody("x", "y", "z", "a"));
    }

    [Subject(typeof(HandledExceptionManager), "Creating and sending email")]
    internal class when_creating_mail_fails : HandledExceptionManagerSpec
    {
        private static Exception Exception;
        private Establish context = () => IMailClient.WhenToldTo(x => x.PrepareAndSend(Arg<MailMessage>.Is.Anything)).Throw(new Exception());

        private Because of = () => Exception = Catch.Exception(() => HandledExceptionManager.SendNotificationEmail("x", "y", "z"));
        
        private It will_eat_the_exception = () => Exception.ShouldBeNull();
    }

    [Subject(typeof(HandledExceptionManager), "ShowDialog")]
    internal class when_populating_the_exception_dialog : HandledExceptionManagerSpec
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
        private Because of = () => HandledExceptionManager.ShowDialog("x", "y", "z");
        private It will_show_the_dialog = () => IExceptionDialog.WasToldTo(x => x.FormShowDialog());
    }


}