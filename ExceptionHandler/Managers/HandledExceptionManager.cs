#region

using System;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using ExceptionHandler.Mail;
using ExceptionHandler.Settings;
using ExceptionHandler.View;

#endregion

namespace ExceptionHandler.Managers
{
    public class HandledExceptionManager
    {
        private readonly IMailClient _mailClient;
        private readonly IExceptionMessageBuilder _exceptionMessageBuilder;
        private readonly IExceptionDialog _exceptionDialog;

        public HandledExceptionManager(IMailClient mailClient, IExceptionMessageBuilder exceptionMessageBuilder, IExceptionDialog exceptionDialog)
        {
            _mailClient = mailClient;
            _exceptionMessageBuilder = exceptionMessageBuilder;
            _exceptionDialog = exceptionDialog;
        }

        protected internal string EmailBody;
        protected internal string ExceptionType;
        internal string WhatHappened;
        internal string HowUserIsAffected;
        internal string WhatUserCanDo;

        private void ExceptionToEmail()
        {
            var mailMessage = new MailMessage
                                  {
                                      Subject = "Handled Exception notification - " + ExceptionType,
                                      Body = EmailBody
                                  };
            try
            {
                _mailClient.PrepareAndSend(mailMessage);
            }
            catch
            {
            }
        }

        private string GetDefaultMore(string strMoreDetails)
        {
            if (!string.IsNullOrEmpty(strMoreDetails)) return strMoreDetails;
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("Technical information follows: " + Environment.NewLine);
            stringBuilder.Append("---" + Environment.NewLine);
            stringBuilder.Append(_exceptionMessageBuilder.SystemInfo());

            return stringBuilder.ToString();
        }

        public void ShowDialog(string strWhatHappened, string strHowUserAffected, string strWhatUserCanDo, string strMoreDetails="")
        {
            _exceptionDialog.Text = _exceptionDialog.Text;
            _exceptionDialog.ErrorBox.Text = strWhatHappened;
            _exceptionDialog.ScopeBox.Text = strHowUserAffected;
            _exceptionDialog.ActionBox.Text = strWhatUserCanDo;
            _exceptionDialog.TxtMore.Text = strMoreDetails;
            _exceptionDialog.Button1.Visible = false;
            _exceptionDialog.Button2.Visible = false;
            _exceptionDialog.Button3.Text = "OK";
            _exceptionDialog.FormAcceptButton = _exceptionDialog.Button3;
            _exceptionDialog.PictureBox1.Image = SystemIcons.Error.ToBitmap();
            _exceptionDialog.FormShowDialog();
        }

        protected internal void SendNotificationEmail(string whatHappened, string howUserAffected, string whatUserCanDo,
                                            string moreDetails="")
        {
            if (AppSettings.DebugMode) return;
            EmailBody = _exceptionMessageBuilder.BuildEmailBody(whatHappened, howUserAffected,
                                                    whatUserCanDo, GetDefaultMore(moreDetails));
            ExceptionToEmail();
        }
    }
}