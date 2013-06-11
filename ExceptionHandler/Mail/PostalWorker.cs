using System;
using Microsoft.Office.Interop.Outlook;

namespace ExceptionHandler.Mail
{


    public class PostalWorker : IPostalWorker
    {
        #region IPostalWorker Members

        public void SendMail(IMailMessage mailMessage)
        {
            var app = new Application();
            var mail = app.CreateItem(OlItemType.olMailItem);
            mail.Body = mailMessage.Body;
            mail.Recipients.Add("somebody@nobody.com");
            mail.Recipients.Add("somebody@nobody.com");
            mailMessage.AttachmentPaths.ForEach(x => mail.Attachments.Add(x));
            mail.Subject = mailMessage.Subject;
            mail.Send();
        }

        #endregion
    }

    
}