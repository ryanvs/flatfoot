namespace ExceptionHandler.Mail
{
    public interface IMailClient
    {
        void PrepareAndSend(MailMessage mailMessage);
    }
}