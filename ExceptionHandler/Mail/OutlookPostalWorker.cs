using System;
using Microsoft.Office.Interop.Outlook;

namespace ExceptionHandler.Mail
{


    public class OutlookPostalWorker : IPostalWorker
    {
        public bool CanCreateMessage
        {
            get
            {
                try
                {
                    var app = new Application();
                    var mail = app.CreateItem(OlItemType.olMailItem);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        #region IPostalWorker Members

        public void SendMail(IMailMessage mailMessage)
        {
            var app = new Application();
            var mail = app.CreateItem(OlItemType.olMailItem);
            mail.Body = mailMessage.Body;
            mailMessage.To.ForEach(x => mail.Recipients.Add(x));
            mailMessage.AttachmentPaths.ForEach(x => mail.Attachments.Add(x));
            mail.Subject = mailMessage.Subject;
            mail.Send();
        }

        #endregion
    }

    
}